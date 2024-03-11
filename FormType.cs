using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using CST.Utill;

namespace SmartOnePass
{

    public partial class FormType : Form
    {
        public FormType()
        {
            InitializeComponent();
        }

        private void FormType_Load(object sender, EventArgs e)
        {
            ConstructionCompany company = JsonSerializer.Instance.LoadAndDeserialize<ConstructionCompany>(Application.StartupPath, "Company");

            this.BackColor = Color.FromArgb(0, 92, 170);

            // 20201-01-20 추가
            // SK 위버 필드 현장에 Web 서버연동이 추가 되어 Company를 999로 설정하여 예외 처리를 함.
            if (int.Parse(company.Type.ToString()) == 999)
            {
                comboBox1.Text = "Web 서버 연동";
                comboBox2.Text = "Web 서버 연동";
                return;
            }

            //comboBox1.SelectedIndex = (company.Type / 100) - 1;
            string _strTypeIndex = company.Type.ToString();

            comboBox1.SelectedIndex = int.Parse(_strTypeIndex.Substring(0,1)) -1;

            for (int i = 0; i < company.Name.Count; i++)
            {
                comboBox2.Items.Add(company.Name[i]);
            }
             
            //comboBox2.SelectedIndex = (company.Type % 100) - 1;
            comboBox2.SelectedIndex = int.Parse(_strTypeIndex.Substring(1,2)) -1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConstructionCompany company = JsonSerializer.Instance.LoadAndDeserialize<ConstructionCompany>(Application.StartupPath, "Company");
            company.Name.Add(textBox1.Text);

            JsonSerializer.Instance.SerializeAndSave<ConstructionCompany>(company, "Company");

            comboBox2.Items.Clear();

            for (int i = 0; i < company.Name.Count; i++)
            {
                comboBox2.Items.Add(company.Name[i]);
            }

            this.Close();
        }

        private void btnSet_Click(object sender, EventArgs e)
        {
            ConstructionCompany company = JsonSerializer.Instance.LoadAndDeserialize<ConstructionCompany>(Application.StartupPath, "Company");
            company.Type = ((comboBox1.SelectedIndex + 1) * 100) + (comboBox2.SelectedIndex + 1);
            JsonSerializer.Instance.SerializeAndSave<ConstructionCompany>(company, "Company");

            Program.g_appType = company.Type;

            this.Close();
        }
    }

    public class ConstructionCompany
    {
        public int Type;
        public List<string> Name = new List<string>();
    }

}
