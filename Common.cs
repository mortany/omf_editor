using System;
using System.Windows.Forms;

public class AutoClosingMessageBox
{
    System.Threading.Timer _timeoutTimer;
    string _caption;
    AutoClosingMessageBox(string text, string caption, int timeout, MessageBoxIcon icon = 0)
    {
        _caption = caption;
        _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
            null, timeout, System.Threading.Timeout.Infinite);
        using (_timeoutTimer)
            MessageBox.Show(text, caption,0, icon);
    }
    public static void Show(string text, string caption, int timeout, MessageBoxIcon icon = 0)
    {
        new AutoClosingMessageBox(text, caption, timeout, icon);
    }
    void OnTimerElapsed(object state)
    {
        IntPtr mbWnd = FindWindow("#32770", _caption); // lpClassName is #32770 for MessageBox
        if (mbWnd != IntPtr.Zero)
            SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        _timeoutTimer.Dispose();
    }
    const int WM_CLOSE = 0x0010;
    [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
    static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
}

namespace OMF_Editor
{
    partial class Form1
    {
        private int BitSet(int flags, int mask, bool bvalue)
        {
            if (bvalue)
                return flags |= mask;
            else
                return flags &= ~mask;
        }

        private void ChangeMotionMarksPanelState(bool state)
        {
            bMotMarkPanel = state;

            foreach (Control control in groupMotomMarks.Controls)
                control.Enabled = state;
        }

        bool bMotionMarksGroupSelected()
        {
            return listMotionMarksGroup.SelectedIndices.Count > 0 && listMotionMarksGroup.Items.Count > 0;
        }

        bool bMotionMarkSelected()
        {
            return listMotionMarksParams.SelectedIndices.Count > 0 && listMotionMarksParams.Items.Count > 0;
        }

        private void MotionMarkPanelReset()
        {
            ChangeMarksParamsPanelState(false);
            listMotionMarksParams.Items.Clear();
            listMotionMarksGroup.Items.Clear();
            ChangeMarksParamsEditState(false);
        }

        private void ChangeMarksParamsPanelState(bool state)
        {
            listMotionMarksParams.Enabled = state;
            btnAddMark.Enabled = state;
            btnDelMark.Enabled = state;
		}

        private void ChangeMarksParamsEditState(bool state)
        {
            boxStartMotionMark.Text = "";
            boxStartMotionMark.Enabled = state;

            boxEndMotionMark.Text = "";
            boxEndMotionMark.Enabled = state;
        }

        private void DisableInput()
        {
            bTextBoxEnabled = false;
            foreach (TextBox box in textBoxes)
            {
                box.Text = "";
                box.Enabled = false;
            }

            foreach (CheckBox box in Boxes)
            {
                box.Enabled = false;
                box.Checked = false;
            }

            groupBox2.Enabled = false;

            ChangeMotionMarksPanelState(false);

            MotionMarkPanelReset();
        }

        private void EnableInput()
        {
            bTextBoxEnabled = true;
            foreach (TextBox box in textBoxes)
            {
                box.Text = "";
                box.Enabled = true;
            }

            foreach (CheckBox box in Boxes)
            {
                box.Enabled = true;
            }

            groupBox2.Enabled = true;
        }
    }
}
