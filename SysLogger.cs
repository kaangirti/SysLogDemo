/* -------------------------------------------------------
--- Codify
--- http://codify.istanbul

--------------------------------------------------------*/

using System.Net.Sockets;
using System.Net;
using System.Text;
using static SysLogDemo.SysLogger.Socket;
using System.Globalization;
using System.Net.Http;

namespace SysLogDemo
{
    public static class SysLogger
    {
        private static List<SocketItem> Data = new List<SocketItem>();
        private static string DefaultSysLogHost = "127.0.0.1";
        private static int DefaultSysLogPort = 514;
        private static ProtocolType DefaultSysLogProtocol = ProtocolType.Tcp;
        private static string DefaultAppHost = "AppHost";
        private static string DefaultAppName = "AppName";
        private static string DefaultSysLogServerName = "Default";        
        private static LogFormat DefaultLogFormat = LogFormat.RFC3164;
        private static string DefaultTextLogPath = Environment.CurrentDirectory + "/Log";
        private static string DefaultTextLogFileName = "#Date#_#Status#.txt";
        private static CultureInfo DefaultCulture = new CultureInfo("en");

        #region --- Add SysLog Server ---

        public static Socket Add(string sysLogServerName)
        {
            return AddSource(new ServerModel()
            {
                AppHost = DefaultAppHost,
                AppName = DefaultAppName,
                LogFormat = DefaultLogFormat,
                SysLogHost = DefaultSysLogHost,
                SysLogPort = DefaultSysLogPort,
                SysLogProtocol = DefaultSysLogProtocol,
                SysLogServerName = sysLogServerName,
                TextLogPath = DefaultTextLogPath,
                TextLogFileName = DefaultTextLogFileName,
            });
        }

        public static Socket Add(string sysLogServerName, string sysLogHost)
        {
            return AddSource(new ServerModel()
            {
                AppHost = DefaultAppHost,
                AppName = DefaultAppName,
                LogFormat = DefaultLogFormat,
                SysLogHost = sysLogHost,
                SysLogPort = DefaultSysLogPort,
                SysLogProtocol = DefaultSysLogProtocol,
                SysLogServerName = sysLogServerName,
                TextLogPath = DefaultTextLogPath,
                TextLogFileName = DefaultTextLogFileName,
            });
        }

        public static Socket Add(string sysLogServerName, string sysLogHost, int sysLogPort)
        {
            return AddSource(new ServerModel()
            {
                AppHost = DefaultAppHost,
                AppName = DefaultAppName,
                LogFormat = DefaultLogFormat,
                SysLogHost = sysLogHost,
                SysLogPort = sysLogPort,
                SysLogProtocol = DefaultSysLogProtocol,
                SysLogServerName = sysLogServerName,
                TextLogPath = DefaultTextLogPath,
                TextLogFileName = DefaultTextLogFileName,
            });
        }

        public static Socket Add(ServerModel serverSettings)
        {
            return AddSource(serverSettings);
        }

        private static Socket AddSource(ServerModel serverSettings)
        {
            bool isExist = Data.Any(x => x.Code == serverSettings.SysLogServerName);
            if (!isExist) Data.Add(new SocketItem() { Code = serverSettings.SysLogServerName, Socket = new Socket(serverSettings) });

            return Get(serverSettings.SysLogServerName);
        }
        #endregion

        #region --- Get SysLog Server ---
        public static Socket Get(string sysLogServerName)
        {
            #pragma warning disable
            SocketItem socketItem = Data.Where(x => x.Code == sysLogServerName).FirstOrDefault();
            if (socketItem != null)
            {
                socketItem.Socket.IsError = false;
                socketItem.Socket.ErrorMessage = String.Empty;
                return socketItem.Socket;
            }
            else
            {
                return Add(sysLogServerName);
            }
        }
        #endregion

        #region --- Remove SysLog Server ---
        public static void Remove(string sysLogServerName)
        {
            #pragma warning disable
            SocketItem socketItem = Data.Where(x => x.Code == sysLogServerName).FirstOrDefault();
            if (socketItem != null)
            {
                Data.Remove(socketItem);
            }
        }
        #endregion

        #region --- SysLog Server Model ---
        private class SocketItem
        {
            public string Code { get; set; }
            public Socket Socket { get; set; }
        }
        #endregion

        public class Socket
        {
            #region --- Statics ---
            private System.Net.Sockets.Socket? SysLogSocket = null;
            private IPEndPoint? RemoteEndPoint = null;
            private ServerModel ServerItem = new ServerModel();
            public bool IsError = false;
            public string ErrorMessage = String.Empty;
            public LogFormat SysLogFormat = DefaultLogFormat;
            #endregion

            #region --- Init ---
            public Socket()
            {
                Init(new ServerModel() {
                    AppHost = DefaultAppHost,
                    AppName = DefaultAppName,
                    LogFormat = SysLogFormat,
                    SysLogHost = DefaultSysLogHost,
                    SysLogPort = DefaultSysLogPort,
                    SysLogProtocol = DefaultSysLogProtocol,
                    SysLogServerName = DefaultSysLogServerName,
                    TextLogPath = DefaultTextLogPath,
                    TextLogFileName = DefaultTextLogFileName,
                });
            }

            public Socket(string sysLogHost)
            {
                Init(new ServerModel()
                {
                    AppHost = DefaultAppHost,
                    AppName = DefaultAppName,
                    LogFormat = SysLogFormat,
                    SysLogHost = sysLogHost,
                    SysLogPort = DefaultSysLogPort,
                    SysLogProtocol = DefaultSysLogProtocol,
                    SysLogServerName = DefaultSysLogServerName,
                    TextLogPath = DefaultTextLogPath,
                    TextLogFileName = DefaultTextLogFileName,
                });
            }

            public Socket(string sysLogHost, int sysLogPort)
            {
                Init(new ServerModel()
                {
                    AppHost = DefaultAppHost,
                    AppName = DefaultAppName,
                    LogFormat = SysLogFormat,
                    SysLogHost = sysLogHost,
                    SysLogPort = sysLogPort,
                    SysLogProtocol = DefaultSysLogProtocol,
                    SysLogServerName = DefaultSysLogServerName,
                    TextLogPath = DefaultTextLogPath,
                    TextLogFileName = DefaultTextLogFileName,
                });
            }

            public Socket(ServerModel serverSettings)
            {
                Init(serverSettings);
            }

            private void Init(ServerModel serverItem)
            {
                IsError = false;
                ErrorMessage = String.Empty;

                ServerItem = serverItem;
            }
            #endregion

            #region --- Connect ----
            public bool Connect()
            {
                bool result = false;
                try
                {
                    IsError = false;
                    ErrorMessage = String.Empty;

                    if (SysLogSocket == null || !SysLogSocket.Connected)
                    {
                        IPAddress ipAddress = IPAddress.Parse(ServerItem.SysLogHost);
                        SysLogSocket = new System.Net.Sockets.Socket(ipAddress.AddressFamily, SocketType.Stream, ServerItem.SysLogProtocol);
                        RemoteEndPoint = new IPEndPoint(ipAddress, ServerItem.SysLogPort);

                        try
                        {
                            SysLogSocket.Connect(RemoteEndPoint);
                        }
                        catch (ArgumentNullException ane)
                        {
                            IsError = true;
                            ErrorMessage = "ArgumentNullException: " + ane.ToString();
                        }
                        catch (SocketException se)
                        {
                            IsError = true;
                            ErrorMessage = "SocketException: " + se.ToString();
                        }
                        catch (Exception e)
                        {
                            IsError = true;
                            ErrorMessage = "Unexpected exception: " + e.ToString();
                        }
                    }

                    result = SysLogSocket.Connected;
                }
                catch (Exception e)
                {
                    IsError = true;
                    ErrorMessage = e.ToString();
                }

                return result;
            }
            #endregion

            #region --- Disconnect ----
            public void Disconnect()
            {
                try
                {
                    IsError = false;
                    ErrorMessage = String.Empty;

                    //SysLogSocket.Shutdown(SocketShutdown.Both);
                    SysLogSocket.Disconnect(true);
                    //SysLogSocket.Close();
                }
                catch (Exception e)
                {
                    IsError = true;
                    ErrorMessage = e.ToString();
                }
            }
            #endregion

            #region --- Send ---
            public void Send(LogModel logModel)
            {
                SendSource(logModel, SendType.Send);
            }

            public void Send(string message)
            {
                LogModel logModel = new LogModel();
                logModel.Description = message;

                SendSource(logModel, SendType.Send);
            }

            public void SendAndClose(LogModel logModel)
            {
                SendSource(logModel, SendType.SendAndClose);
            }

            public void SendAndClose(string message)
            {
                LogModel logModel = new LogModel();
                logModel.Description = message;

                SendSource(logModel, SendType.SendAndClose);
            }

            private async void SendSource(LogModel logModel, SendType sendType)
            {
                try
                {
                    IsError = false;
                    ErrorMessage = String.Empty;

                    #region --- Prepare Message ---
                    string message = "";

                    if (!String.IsNullOrEmpty(logModel.Title)) logModel.Title = logModel.Title.Trim().Replace("\r\n", "(enter)").Replace("\n", "(enter)").Replace("\r", "(enter)");
                    if (!String.IsNullOrEmpty(logModel.Description)) logModel.Description = logModel.Description.Trim().Replace("\r\n", "(enter)").Replace("\n", "(enter)").Replace("\r", "(enter)");
                    if (!String.IsNullOrEmpty(logModel.JsonDetail)) logModel.JsonDetail = logModel.JsonDetail.Trim().Replace("\r\n", "(enter)").Replace("\n", "(enter)").Replace("\r", "(enter)");

                    int priorityNumber = ((int)logModel.Facility * 8) + (int)logModel.Severity;

                    if (SysLogFormat == LogFormat.RFC3164)
                    {
                        // PRI — or "priority", is a number calculated from Facility(what kind of message) code and Severity(how urgent is the message) code: PRI = Facility * 8 + Severity

                        string timestamp = null;
                        var dt = DateTime.Now;
                        var day = dt.Day < 10 ? " " + dt.Day : dt.Day.ToString();
                        timestamp = String.Concat(dt.ToString("MMM "), day, dt.ToString(" HH:mm:ss"));

                        // FORMAT ->     <PRIORITY>(No Space)TIMESTAMP(Mmm dd hh:mm:ss) HOSTNAME MSG(TAG):MSG(CONTENT)
                        message = "<" + priorityNumber + ">" + timestamp + " " + ServerItem.AppHost + " " + logModel.Title + ":" + logModel.Description + " | " + logModel.JsonDetail + "";
                    }
                    else if (SysLogFormat == LogFormat.RFC5424)
                    {
                        // TIMESTAMP — valid timestamp examples (must follow ISO 8601 format with uppercase "T" and "Z")
                        // 1985-04-12T23:20:50.52Z
                        // 2003-08-24T05:14:15.000003-07:00
                        // - ("nil" value) if time not available

                        // FORMAT ->     <PRIORITY>(No Space)VERSION TIMESTAMP HOSTNAME APP-NAME PROCID(-) [MSGID STRUCTURED-DATA] MSG
                        // EXAMPLE ->    <1>12 2003-10-11T22:14:15.003Z example.com entslog ID47 [example@SDID@32473 iut="3" eventSource="Application" eventID="1011"] This is a test message.
                        message = "<" + priorityNumber + ">" + ServerItem.AppName + " " + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.ffffffK") + " " + ServerItem.AppHost + " " + ServerItem.AppName + " - " + logModel.Title + " [" + logModel.JsonDetail + "] " + logModel.Description;
                    }
                    else
                    {
                        message = logModel.Title + logModel.Description + logModel.JsonDetail;
                    }
                    #endregion

                    // Socket status...
                    bool lastConnectStatus = Connect();
                    if (lastConnectStatus) // Save on socket server
                    {
                        int bytesSent = await SysLogSocket.SendAsync(Encoding.ASCII.GetBytes(message.ClearTurkishCharacter() + "\r\n"));
                        if (sendType == SendType.SendAndClose)
                        {
                            Disconnect();
                            lastConnectStatus = false;
                        }

                        #region --- Send Text Logs ---
                        Task.Factory.StartNew(() =>
                        {
                            if (Directory.Exists(ServerItem.TextLogPath))
                            {
                                foreach (string fileItem in Directory.GetFiles(ServerItem.TextLogPath))
                                {
                                    string[] allText = System.IO.File.ReadAllLines(fileItem);
                                    foreach (string logItem in allText)
                                    {
                                        try
                                        {
                                            lastConnectStatus = Connect();
                                            if (lastConnectStatus)
                                            {
                                                bytesSent = SysLogSocket.Send(Encoding.ASCII.GetBytes(logItem.ClearTurkishCharacter() + "\r\n"));
                                                if (sendType == SendType.SendAndClose)
                                                {
                                                    Disconnect();
                                                    lastConnectStatus = false;
                                                }

                                                string newLogItem = logItem.Trim();
                                                var newLines = System.IO.File.ReadAllLines(fileItem).Where(line => line.Trim() != newLogItem).ToArray();
                                                System.IO.File.WriteAllLines(fileItem, newLines);
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            break;
                                        }
                                    }

                                    int lineCount = System.IO.File.ReadAllLines(fileItem).Count();
                                    if (lineCount == 0) System.IO.File.Delete(fileItem);
                                }
                            }
                        });
                        #endregion

                        IsError = false;
                        ErrorMessage = String.Empty;
                    }
                    else // Save on file
                    {
                        if (!String.IsNullOrEmpty(ServerItem.TextLogPath) && ServerItem.TextLogPath.Substring(ServerItem.TextLogPath.Length - 1, 1) != "/") 
                            ServerItem.TextLogPath = ServerItem.TextLogPath + "/";

                        // Prepare filename
                        if (String.IsNullOrEmpty(ServerItem.TextLogFileName)) ServerItem.TextLogFileName = DateTime.Now.ToString("yyyy_MM_dd") + "_" + logModel.Severity.ToString().ToLower(DefaultCulture) + ".txt";
                        else ServerItem.TextLogFileName = ServerItem.TextLogFileName.Replace("#Date#", DateTime.Now.ToString("yyyy_MM_dd")).Replace("#Hour#", DateTime.Now.ToString("hh")).Replace("#Minute#", DateTime.Now.ToString("mm")).Replace("#Status#", logModel.Severity.ToString().ToLower(DefaultCulture));

                        // Saved text file
                        if (!Directory.Exists(ServerItem.TextLogPath)) Directory.CreateDirectory(ServerItem.TextLogPath);
                        System.IO.File.AppendAllText(ServerItem.TextLogPath + ServerItem.TextLogFileName, message + Environment.NewLine);

                        IsError = true;
                        ErrorMessage = "Connection lost, data saved as text.";
                    }
                }
                catch (Exception e)
                {
                    IsError = true;
                    ErrorMessage = e.ToString();
                }
            }
            #endregion

            #region --- Enums ---
            private enum SendType
            {
                Send,
                SendAndClose
            }

            public enum Severity
            {
                Emergency = 0,
                Alert = 1,
                Critical = 2,
                Error = 3,
                Warning = 4,
                Notice = 5,
                Info = 6,
                Debug = 7
            }

            public enum Facility
            {
                KernelMessages = 0,
                UserLevelMessages = 1,
                MailSystem = 2,
                SystemDaemons = 3,
                SecurityOrAuthorizationMessages1 = 4,
                InternalMessages = 5,
                LinePrinterSubsystem = 6,
                NetworkNewsSubsystem = 7,
                UUCPSubsystem = 8,
                ClockDaemon1 = 9,
                SecurityOrAuthorizationMessages2 = 10,
                FTPDaemon = 11,
                NTPSubsystem = 12,
                LogAudit = 13,
                LogAlert = 14,
                ClockDaemon2 = 15,
                LocalUse0 = 16,
                LocalUse1 = 17,
                LocalUse2 = 18,
                LocalUse3 = 19,
                LocalUse4 = 20,
                LocalUse5 = 21,
                LocalUse6 = 22,
                LocalUse7 = 23
            }

            public enum LogFormat
            {
                RFC5424 = 0,
                RFC3164 = 1,
                Custom = 2
            }
            #endregion

            #region --- Models ---
            public class LogModel
            {
                public string Title { get; set; } = String.Empty;
                public string Description { get; set; } = String.Empty;
                public string JsonDetail { get; set; } = String.Empty;
                public Severity Severity { get; set; } = Severity.Info;
                public Facility Facility { get; set; } = Facility.UserLevelMessages;
            }

            public class ServerModel
            {
                public string SysLogServerName { get; set; } = DefaultSysLogServerName;
                public string SysLogHost { get; set; } = DefaultSysLogHost;
                public int SysLogPort { get; set; } = DefaultSysLogPort;
                public ProtocolType SysLogProtocol { get; set; } = DefaultSysLogProtocol;
                public string AppHost { get; set; } = DefaultAppHost;
                public string AppName { get; set; } = DefaultAppName;
                public LogFormat LogFormat { get; set; } = DefaultLogFormat;
                public string TextLogPath { get; set; } = DefaultTextLogPath;
                public string TextLogFileName { get; set; } = DefaultTextLogFileName;
            }
            #endregion
        }

        #region --- Helpers ---
        private static string ClearTurkishCharacter(this string value)
        {
            string result = "";
            if (!String.IsNullOrEmpty(value))
            {
                value = value.Replace("ş", "s");
                value = value.Replace("ö", "o");
                value = value.Replace("ü", "u");
                value = value.Replace("ı", "i");
                value = value.Replace("ç", "c");
                value = value.Replace("ğ", "g");
                value = value.Replace("Ş", "s");
                value = value.Replace("Ö", "o");
                value = value.Replace("Ü", "u");
                value = value.Replace("İ", "i");
                value = value.Replace("Ç", "c");
                value = value.Replace("Ğ", "g");
                result = value;
            }
            return result;
        }
        #endregion
    }
}
