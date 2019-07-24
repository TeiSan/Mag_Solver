using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MAG_Solver
{
    public partial class MagSolverForm : Form
    {
        public MagSolverForm()
        {
            InitializeComponent();
        }

        private void ButtonExec_Click(object sender, EventArgs e)
        {
            //入力値チェック
            string seedString = "";

            seedString = this.textBox5.Text +
                         this.textBoxHolizontalKey1.Text +
                         this.textBoxHolizontalKey2.Text +
                         this.textBoxHolizontalKey3.Text +
                         this.textBoxVarticalKey1.Text +
                         this.textBoxVarticalKey2.Text +
                         this.textBoxVarticalKey3.Text;
            
            int seed = 0;
            if (!int.TryParse(seedString, out seed))
                return;
            MagProbremsKey key = new MagProbremsKey(seed);

            //計算処理実行
            List<MagProbremsAnswer> answers = MagSolver.Solve(key);

            if (answers.Count > 0)
            {
                MagProbremsAnswer answer = answers[0];

                this.textBox1.Text = answer.UpperLeftValue.ToString();
                this.textBox2.Text = answer.UpperCenterValue.ToString();
                this.textBox3.Text = answer.UpperRightValue.ToString();

                this.textBox4.Text = answer.MiddleLeftValue.ToString();
                this.textBox6.Text = answer.MiddleRightValue.ToString();

                this.textBox7.Text = answer.LowerLeftValue.ToString();
                this.textBox8.Text = answer.LowerCenterValue.ToString();
                this.textBox9.Text = answer.LowerRightValue.ToString();
            }

            StringBuilder sb = new StringBuilder();
            foreach(MagProbremsAnswer answer in answers)
            {
                sb.AppendFormat("{1}{2}{3}{0}{4}{5}{6}{0}{7}{8}{9}{0}{0}", 
                    Environment.NewLine,
                    answer.UpperLeftValue, answer.UpperCenterValue, answer.UpperRightValue,
                    answer.MiddleLeftValue, answer.MiddleCenterValue, answer.MiddleRightValue,
                    answer.LowerLeftValue, answer.LowerCenterValue, answer.LowerRightValue);
            }
            this.AnswerList.Text = sb.ToString();

            this.textBox5.Focus();
            this.textBox5.SelectAll();
        }

        private void TextBoxes_Enter(object sender, EventArgs e)
        {
            if (sender is TextBox)
                ((TextBox)sender).SelectAll();
        }

        private void TextBoxes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < '0' || '9' < e.KeyChar ) &&
                e.KeyChar != '\n')
            {
                e.Handled = true;
            }
        }

        private void TextBoxes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
                this.SelectNextControl(((Control)sender), true, true, true, true);
        }

        private void TextBoxes_TextChanged(object sender, EventArgs e)
        {
            this.SelectNextControl(((Control)sender), true, true, true, true);
        }
    }
}
