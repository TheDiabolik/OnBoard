using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnBoard
{
    class DisplayManager
    {
        public static void RichTextBoxInvoke(RichTextBox richTextBox, string infoText, Color selectionColor)
        {
            try
            {

                if (!richTextBox.IsHandleCreated || richTextBox.IsDisposed)
                    return;

                if (richTextBox.InvokeRequired)
                    richTextBox.Invoke((MethodInvoker)delegate
                    {

                        //richTextBox.Document.SelectionColor = selectionColor;
                        //richTextBox.AppendText("-------------------------------------------" + "\n");
                        richTextBox.AppendText(infoText + "\n");
                        //richTextBox.AppendText("-------------------------------------------" + "\n");

                    });
                else
                {
                    //richTextBox.Document.SelectionColor = selectionColor;
                    //richTextBox.AppendText("-------------------------------------------" + "\n");
                    richTextBox.AppendText(infoText + "\n");
                    //richTextBox.AppendText("-------------------------------------------" + "\n");

                }
            }
            catch
            {
            }
        }

        public static void LabelInvoke(Label label, string text)
        {
            if (label.InvokeRequired)
                label.Invoke((MethodInvoker)delegate
                {
                    label.Text = text;
                });
            else
            {
                label.Text = text;
            }
        }

        public static void TextBoxInvoke(TextBox textBox, string text)
        {
            if (textBox.InvokeRequired)
                textBox.Invoke((MethodInvoker)delegate
                {
                    textBox.Text = text;
                });
            else
            {
                textBox.Text = text;
            }
        }


        public static void ListViewInvoke(ListView listView, string text)
        {
            if (listView.InvokeRequired)
                listView.Invoke((MethodInvoker)delegate
                {
                    //label.Text = text;
                });
            else
            {
                //label.Text = text;
            }
        }

       

        public static object ComboBoxGetSelectedItemInvoke(ComboBox combobox)
        {
            object selectedItem = null;

            MethodInvoker miClearItems = delegate
            {
                selectedItem = combobox.SelectedItem;
            };

            if (combobox.InvokeRequired)
            {
                combobox.Invoke(miClearItems);
            }
            else
            {
                miClearItems();
            }

            return selectedItem;
        }

        public static void ComboBoxInvoke(ComboBox comboBox, string text)
        {
            if (comboBox.InvokeRequired)
                comboBox.Invoke((MethodInvoker)delegate
                {
                    //label.Text = text;
                });
            else
            {
                //label.Text = text;
            }
        }
    }
}
