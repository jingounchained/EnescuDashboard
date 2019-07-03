using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using GeniePlugin.Interfaces;

namespace EnescuDashboard
{
    public class Program : IPlugin
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        string _NAME = "Enescu Dashboard";
        string _VERSION = "1.0";
        string _AUTHOR = "Djordje";
        string _DESCRIPTION = "Provides a better way to manage/define global variables saved for scripts.";

        public System.Windows.Forms.Form _parent;       //Required for plugin
        public Dashboard _form;

        public void OpenWindow(Form parent)
        {
            if (_form == null || _form.IsDisposed)
            {
                _form = new Dashboard();
            }
            _form.Show();
        }

        #region Interface Properties

        public void Initialize(IHost host)
        {
            Genie.Instance.SetHost(ref host);
            _parent = Genie.Instance.ParentForm;
        }

        public string Name
        {
            get { return _NAME; }
        }

        public string Version
        {
            get { return _VERSION; }
        }

        public string Description
        {
            get { return _DESCRIPTION; }
        }

        public string Author
        {
            get { return _AUTHOR; }
        }

        private bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }
        #endregion
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Dashboard());
        }

        public void Show()
        {
            OpenWindow(Genie.Instance.ParentForm);
        }

        public void VariableChanged(string variable)
        {

        }

        public string ParseText(string text, string window)
        {
            return text;
        }

        public string ParseInput(string text)
        {
            return text;
        }

        public void ParseXML(string xml)
        {

        }

        public void ParentClosing()
        {

        }

    }
}
