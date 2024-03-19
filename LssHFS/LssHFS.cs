using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LssHFS
{
    public partial class LssHFS : Form
    {
        //스레드 중지를 위해
        public static CancellationTokenSource cts;
        public static CancellationToken token;
        HttpListener listener = null;

        public LssHFS()
        {
            InitializeComponent();
            GetIPAddress();

            CBO_아이피.SelectedIndex = 0;
            TXT_포트.Text = "9999";
            TXT_홈경로.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        private void BTN_시작_Click(object sender, EventArgs e)
        {
            if (LssCheck())
            {
                var str = CBO_아이피.Text;                
                LssServer(str);               

                BTN_중지.Enabled = true;
                BTN_시작.Enabled = false;
                CBO_아이피.Enabled = false;
                BTN_열기.Enabled = false;
            }
        }

        private void BTN_열기_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if(folderBrowser.ShowDialog() == DialogResult.OK)
            {
                TXT_홈경로.Text = folderBrowser.SelectedPath;
            }
        }

        public void GetIPAddress()
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            string empty = string.Empty;
            for (int i = 0; i < (int)hostEntry.AddressList.Length; i++)
            {
                if (hostEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    empty = hostEntry.AddressList[i].ToString();
                    CBO_아이피.Items.Add(empty);
                }
            }
        }

        public Boolean LssCheck()
        {
            if (CBO_아이피.Text.Equals(""))
            {
                MessageBox.Show("아이피 선택하세요.");
                return false;
            }

            if(TXT_포트.Text.Length == 0)
            {
                MessageBox.Show("포트번호를 입력하세요.");
                return false;
            }

            if(TXT_홈경로.Text.Length == 0)
            {
                MessageBox.Show("홈디렉토리를 설정하세요.");
                return false;
            }

            return true;
        }

        public void LssServer(string Ipaddr)
        {
            try
            {
                listener = new HttpListener();
                int Port = Convert.ToInt32(TXT_포트.Text);
                listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous | AuthenticationSchemes.Basic;
                //listener.Prefixes.Add($"http://localhost:{Port}/");
                listener.Prefixes.Add($"http://{Ipaddr}:{Port}/");
                listener.Start();
                cts = new CancellationTokenSource();
                token = cts.Token;
                Task task = Task.Run(() =>
                {
                    while (!token.IsCancellationRequested)
                    {
                        try
                        {
                            // 클라이언트로부터의 요청 대기
                            HttpListenerContext context = listener.GetContext();
                            HttpListenerRequest request = context.Request;
                            HttpListenerResponse response = context.Response;

                            // 요청된 파일 또는 폴더 경로 가져오기
                            string requestedPath = request.Url.AbsolutePath;
                            string fullPath = Path.Combine(TXT_홈경로.Text, requestedPath.TrimStart('/').Replace('/', '\\'));

                            // 폴더인지 파일인지 확인
                            if (Directory.Exists(fullPath))
                            {
                                // 폴더인 경우 폴더 내용 목록 반환
                                string[] files = Directory.GetFiles(fullPath);
                                string[] directories = Directory.GetDirectories(fullPath);

                                StringBuilder htmlBuilder = new StringBuilder();
                                htmlBuilder.Append("<html><style>");
                                htmlBuilder.Append(".file{color:blue;}");
                                htmlBuilder.Append(".folder{color:red;}");
                                htmlBuilder.Append(" ul{list-style-type : none; font-family:'돋움'; font-size:11pt;}");
                                htmlBuilder.Append(" li{height:25;}");
                                htmlBuilder.Append("</style><body><ul>");

                                // 상위 폴더로 이동하는 링크 추가
                                htmlBuilder.Append("<li><a href=\"../\">[ ↑ ]</a></li>");

                                // 하위 폴더 목록 추가
                                foreach (string directory in directories)
                                {
                                    string directoryName = Path.GetFileName(directory);
                                    htmlBuilder.Append($"<li><a href=\"{directoryName}/\" class=\"folder\">[{directoryName}]</a></li>");
                                }

                                // 파일 목록 추가
                                foreach (string file in files)
                                {
                                    string fileName = Path.GetFileName(file);
                                    htmlBuilder.Append($"<li>&nbsp;&nbsp;<a href=\"{fileName}\" class=\"file\">{fileName}</a></li>");
                                }

                                htmlBuilder.Append("</ul></body></html>");

                                // HTML 응답 전송
                                byte[] buffer = Encoding.UTF8.GetBytes(htmlBuilder.ToString());
                                response.ContentLength64 = buffer.Length;
                                response.ContentType = "text/html; charset=utf-8";
                                response.OutputStream.Write(buffer, 0, buffer.Length);
                                response.Close();
                            }
                            else if (File.Exists(fullPath))
                            {
                                // 파일인 경우 파일 다운로드
                                Stream fileStream = File.OpenRead(fullPath);
                                response.ContentLength64 = fileStream.Length;
                                response.ContentType = "application/octet-stream";
                                response.AddHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(fullPath));
                                fileStream.CopyTo(response.OutputStream);
                                fileStream.Close();
                                response.OutputStream.Close();
                                response.Close();
                            }
                            else
                            {
                                // 경로가 잘못된 경우 404 에러 반환
                                string errorResponse = "<html><body><h1>404 - Not Found</h1></body></html>";
                                byte[] buffer = Encoding.UTF8.GetBytes(errorResponse);
                                response.StatusCode = (int)HttpStatusCode.NotFound;
                                response.ContentLength64 = buffer.Length;
                                response.ContentType = "text/html; charset=utf-8";
                                response.OutputStream.Write(buffer, 0, buffer.Length);
                                response.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            throw ex;
                        }
                    }
                }, token);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw ex;
            }
            finally
            {
                //if (listener != null) listener.Close();
            }
        }
                
        private void BTN_중지_Click(object sender, EventArgs e)
        {
            cts.Cancel();
            listener.Close();
            BTN_중지.Enabled = false;
            BTN_시작.Enabled = true;
            CBO_아이피.Enabled = true;
            BTN_열기.Enabled = true;
        }
    }
}
