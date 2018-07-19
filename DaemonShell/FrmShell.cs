using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DaemonShell
{
    public partial class FrmShell : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        private List<Process> processes = new List<Process>();

        public FrmShell()
        {
            InitializeComponent();
        }

        private void FrmShell_Load(object sender, EventArgs e)
        {
        }

        private void FrmShell_Shown(object sender, EventArgs e)
        {
            var path = @"C:\Program Files (x86)\Netease\CloudMusic\cloudmusic.exe";

            var process = Process.Start(path);
            IntPtr ptr = IntPtr.Zero;
            //process.WaitForInputIdle();
            while (process.MainWindowHandle == IntPtr.Zero)
            {
                Thread.Sleep(1);
            }
            SetParent(process.MainWindowHandle, this.panelShell.Handle);
            MoveWindow(process.MainWindowHandle, 0, 0, this.Width - 90, this.Height, true);
            this.Text = process.MainWindowTitle;

            processes.Add(process);
        }

        private void FrmShell_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var p in this.processes)
            {
                p.Kill();
            }
        }
    }
}
