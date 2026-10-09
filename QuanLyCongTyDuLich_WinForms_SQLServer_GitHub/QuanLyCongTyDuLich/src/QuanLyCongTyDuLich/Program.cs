using System;
using System.Windows.Forms;
using QuanLyCongTyDuLich.Forms;
namespace QuanLyCongTyDuLich
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += (s, e) => MessageBox.Show("Loi: " + e.Exception.Message, "Thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Application.Run(new FrmMain());
        }
    }
}
