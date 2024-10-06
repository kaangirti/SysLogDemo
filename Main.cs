namespace SysLogDemo
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

            #pragma warning disable
            ddlSerializerChanged(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtConsole.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + " | Gönderim iþlemi baþladý..." + Environment.NewLine + txtConsole.Text;
            string message = txtMessageTitle.Text;

            try
            {
                SysLogger.Socket.LogFormat logFormat = SysLogger.Socket.LogFormat.RFC3164;
                if (txtSocketSerializer.Text == "RFC3164") logFormat = SysLogger.Socket.LogFormat.RFC3164;
                else if (txtSocketSerializer.Text == "RFC5424") logFormat = SysLogger.Socket.LogFormat.RFC5424;
                else if (txtSocketSerializer.Text == "CUSTOM") logFormat = SysLogger.Socket.LogFormat.Custom;

                SysLogger.Socket.Severity severity = SysLogger.Socket.Severity.Info;
                if (txtStatus.Text == "INFO") severity = SysLogger.Socket.Severity.Info;
                else if (txtStatus.Text == "WARNING") severity = SysLogger.Socket.Severity.Warning;
                else if (txtStatus.Text == "ERROR") severity = SysLogger.Socket.Severity.Error;

                SysLogger.Socket.Facility facility = SysLogger.Socket.Facility.UserLevelMessages;
                if (txtFacility.Text == "UserLevelMessages") facility = SysLogger.Socket.Facility.UserLevelMessages;
                else if (txtFacility.Text == "KernelMessages") facility = SysLogger.Socket.Facility.KernelMessages;
                else if (txtFacility.Text == "MailSystem") facility = SysLogger.Socket.Facility.MailSystem;
                else if (txtFacility.Text == "SystemDaemons") facility = SysLogger.Socket.Facility.SystemDaemons;
                else if (txtFacility.Text == "SecurityOrAuthorizationMessages1") facility = SysLogger.Socket.Facility.SecurityOrAuthorizationMessages1;
                else if (txtFacility.Text == "InternalMessages") facility = SysLogger.Socket.Facility.InternalMessages;
                else if (txtFacility.Text == "LinePrinterSubsystem") facility = SysLogger.Socket.Facility.LinePrinterSubsystem;
                else if (txtFacility.Text == "NetworkNewsSubsystem") facility = SysLogger.Socket.Facility.NetworkNewsSubsystem;
                else if (txtFacility.Text == "UUCPSubsystem") facility = SysLogger.Socket.Facility.UUCPSubsystem;
                else if (txtFacility.Text == "ClockDaemon1") facility = SysLogger.Socket.Facility.ClockDaemon1;
                else if (txtFacility.Text == "SecurityOrAuthorizationMessages2") facility = SysLogger.Socket.Facility.SecurityOrAuthorizationMessages2;
                else if (txtFacility.Text == "FTPDaemon") facility = SysLogger.Socket.Facility.FTPDaemon;
                else if (txtFacility.Text == "NTPSubsystem") facility = SysLogger.Socket.Facility.NTPSubsystem;
                else if (txtFacility.Text == "LogAudit") facility = SysLogger.Socket.Facility.LogAudit;
                else if (txtFacility.Text == "LogAlert") facility = SysLogger.Socket.Facility.LogAlert;
                else if (txtFacility.Text == "ClockDaemon2") facility = SysLogger.Socket.Facility.ClockDaemon2;
                else if (txtFacility.Text == "LocalUse0") facility = SysLogger.Socket.Facility.LocalUse0;
                else if (txtFacility.Text == "LocalUse1") facility = SysLogger.Socket.Facility.LocalUse1;
                else if (txtFacility.Text == "LocalUse2") facility = SysLogger.Socket.Facility.LocalUse2;
                else if (txtFacility.Text == "LocalUse3") facility = SysLogger.Socket.Facility.LocalUse3;
                else if (txtFacility.Text == "LocalUse4") facility = SysLogger.Socket.Facility.LocalUse4;
                else if (txtFacility.Text == "LocalUse5") facility = SysLogger.Socket.Facility.LocalUse5;
                else if (txtFacility.Text == "LocalUse6") facility = SysLogger.Socket.Facility.LocalUse6;
                else if (txtFacility.Text == "LocalUse7") facility = SysLogger.Socket.Facility.LocalUse7;

                // Server Settings
                //SysLogger.Remove("test");
                SysLogger.Socket loggerSocket = SysLogger.Add(new SysLogger.Socket.ServerModel() { 
                    SysLogServerName = "test", 
                    SysLogHost = txtHost.Text,
                    SysLogPort = Convert.ToInt32(txtPort.Text),
                    SysLogProtocol = txtProtocol.Text == "TCP" ? System.Net.Sockets.ProtocolType.Tcp : System.Net.Sockets.ProtocolType.Udp,
                    AppHost = txtHost.Text,
                    AppName = txtAppName.Text,
                    LogFormat = logFormat
                });
                if (loggerSocket.IsError) txtConsole.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + " | " + loggerSocket.ErrorMessage + Environment.NewLine + txtConsole.Text;

                // Send Log Message
                loggerSocket.Send(new SysLogger.Socket.LogModel() { Facility = facility, Severity = severity, Title = txtMessageTitle.Text, Description = txtMessageDescription.Text, JsonDetail = txtMessageJSON.Text });
                if (loggerSocket.IsError) txtConsole.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + " | " + loggerSocket.ErrorMessage + Environment.NewLine + txtConsole.Text;
                else txtConsole.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss") + " | Gönderildi." + Environment.NewLine + txtConsole.Text;
            }
            catch (Exception ex)
            {
                txtConsole.Text = DateTime.Now.ToString("HH:mm:ss") + " | " + ex.Message + Environment.NewLine + txtConsole.Text;
            }
        }

        private void ddlSerializerChanged(object sender, EventArgs e)
        {
            if (txtSocketSerializer.Text == "RFC3164")
            {
                txtMessageTitle.Text = "Method.Name";
                txtMessageDescription.Text = "Error message...";
                txtMessageJSON.Text = "{ \"Param1\": 123, \"Param2\": \"test\" }";
            }
            else if (txtSocketSerializer.Text == "RFC5424")
            {
                txtMessageTitle.Text = "Method.Name";
                txtMessageDescription.Text = "Error message...";
                txtMessageJSON.Text = "example@SDID@32473 iut=\"3\" eventSource=\"Application\" eventID=\"1011\"";
            }
            else if (txtSocketSerializer.Text == "CUSTOM")
            {
                txtMessageTitle.Text = "";
                txtMessageDescription.Text = "";
                txtMessageJSON.Text = "";
            }
        }
    }
}