using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmartOnePass
{
    public partial class FormConfigInit : Form
    {
        public FormConfigInit()
        {
            InitializeComponent();
        }

        private void FormConfigInit_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(0, 92, 170);

            string[] lines = System.IO.File.ReadAllLines(Application.StartupPath + "\\Config.ini");

            foreach (string _line in lines)
            {
                string[] _str = _line.Split('=');

                string _case = _str[0].ToLower();

                ListViewItem lvi = new ListViewItem("");

                Console.WriteLine(_case);

                switch (_case)
                {
                    case "dbip":
                        lvi.SubItems.Add("DB Ip");
                        lvi.SubItems.Add(_str[1]);
                        lvi.SubItems.Add("사용할 DB IP");
                        listView.Items.Add(lvi);
                        
                        break;
                    case "crt":
                        lvi.SubItems.Add("CRT");
                        lvi.SubItems.Add(_str[1]);
                        lvi.SubItems.Add("CRT 이름(Hyundai, Thyssen, Han, OTS 업체명 입력(대소문자 구분필요))");
                        listView.Items.Add(lvi);
                        break;
                    case "crtip":
                        lvi.SubItems.Add("Crt Ip");
                        lvi.SubItems.Add(_str[1]);
                        lvi.SubItems.Add("Crt 서버 IP");
                        listView.Items.Add(lvi);
                        break;
                    case "subcrtuse":
                        lvi.SubItems.Add("Sub Crt Use");
                        lvi.SubItems.Add(_str[1]);
                        lvi.SubItems.Add("0: Crt 서버 1대 운영일 때 / 1: Crt 서버 2대 운영일 때");
                        listView.Items.Add(lvi);
                        break;
                    case "subip":
                        lvi.SubItems.Add("Sub Ip");
                        lvi.SubItems.Add(_str[1]);
                        lvi.SubItems.Add("서브 Crt 서브 IP");
                        listView.Items.Add(lvi);
                        break;
                    case "crtport":
                        lvi.SubItems.Add("Crt Port");
                        lvi.SubItems.Add(_str[1]);
                        lvi.SubItems.Add("Crt 서버 Port(29600, 2999, 3000, 7128......)");
                        listView.Items.Add(lvi);
                        break;
                    case "subport":
                        lvi.SubItems.Add("Sub Port");
                        lvi.SubItems.Add(_str[1]);
                        lvi.SubItems.Add("서브 Crt 서버 Port(29600, 2999, 3000, 7128......)");
                        listView.Items.Add(lvi);
                        break;
                    case "subcarstart":
                        lvi.SubItems.Add("Sub Car Start");
                        lvi.SubItems.Add(_str[1]);
                        lvi.SubItems.Add("서브 Crt 시작 호기(Sub Crt서버에서 1호기로 시작할 때 / (car+1)-carStart");
                        listView.Items.Add(lvi);
                        break;
                    case "carcount":
                        lvi.SubItems.Add("Car Count");
                        lvi.SubItems.Add(_str[1]);
                        lvi.SubItems.Add("E/V 총 호기수");
                        listView.Items.Add(lvi);
                        break;
                    case "direction_set":
                        lvi.SubItems.Add("Direction Set");
                        lvi.SubItems.Add(_str[1]);
                        lvi.SubItems.Add("0: RF 방향성 리더기 사용 안함 / 1: RF 방향성 리더기 사용함.");
                        listView.Items.Add(lvi);
                        break;
                    case "takeoff_set":
                        lvi.SubItems.Add("TakeOff Set");
                        lvi.SubItems.Add(_str[1]);
                        lvi.SubItems.Add("0:목적층 콜 안함 / 1: 목적층 콜 함");
                        listView.Items.Add(lvi);
                        break;
                    case "subcrtcarflow":
                        lvi.SubItems.Add("Sub Crt Car Flow");
                        lvi.SubItems.Add(_str[1]);
                        lvi.SubItems.Add("0: 서브 Crt 1호기로 호출 / 1: 서브Crt 메인 Crt 마지막 호기에서 이어서 호출");
                        listView.Items.Add(lvi);
                        break;
                    case "crtcalldongho":
                        lvi.SubItems.Add("Crt Call Dong Ho");
                        lvi.SubItems.Add(_str[1]);
                        lvi.SubItems.Add("0: UkrId 콜 방식 / 1: 동호콜 방식(출입문 하나인데 A, B 라인 갈라질 때)");
                        listView.Items.Add(lvi);
                        break;
                    case "spktypeuse":
                        lvi.SubItems.Add("Spk Type Use");
                        lvi.SubItems.Add(_str[1]);
                        lvi.SubItems.Add("0: Spk 등록기 아닐 때 / 1: Spk BLE 등록기 일 때");
                        listView.Items.Add(lvi);
                        break;
                    case "difsvruse":
                        lvi.SubItems.Add("Dif Svr Use");
                        lvi.SubItems.Add(_str[1]);
                        lvi.SubItems.Add("0: 종합수신제어기 서버 사용 안 함 / 1: 종합수신제어기를 사용함(Lobby_Info에 Ip 값을 초기화 하지 않음)");
                        listView.Items.Add(lvi);
                        break;
                    case "keytypeuse":
                        lvi.SubItems.Add("Key Type Use");
                        lvi.SubItems.Add(_str[1]);
                        lvi.SubItems.Add("0: KeyType을 사용하지 않게 하면, 목적층 호출이 되지 않음. / 1: 셋팅이 되어 있으면, TakeOff에 따라 목적층을 호출함.");
                        listView.Items.Add(lvi);
                        break;
                    case "registerdevicetype":
                        lvi.SubItems.Add("Register Device Type");
                        lvi.SubItems.Add(_str[1]);
                        lvi.SubItems.Add("0: 듀얼아이와 공동현관 BLE 등록기를 따로 사용  / 1: 키와 스마트폰 BLE를 통합 등록기");
                        listView.Items.Add(lvi);
                        break;
                    case "setopenid":
                        lvi.SubItems.Add("Set Open ID");
                        lvi.SubItems.Add(_str[1]);
                        lvi.SubItems.Add("0: 사용안함.  / 1: 해당로비에 인증실패시, Key_Info_Master에 ID가 있으면 자동 등록");
                        listView.Items.Add(lvi);
                        break;
                    case "crtcalldonghogroup":
                        lvi.SubItems.Add("Crt Call Dong_Ho Group");
                        lvi.SubItems.Add(_str[1]);
                        lvi.SubItems.Add("0: 사용안함.  / 1: 동호로 E/V 호출시 E/V 그룹으로 되어 있으면 셋팅 필요. kms.Dong_Crt_CarId 참조");
                        listView.Items.Add(lvi);
                        break;
                        /*
                    case "phonenfcuse":
                        lvi.SubItems.Add("SmartPhone NFC USE");
                        lvi.SubItems.Add(_str[1]);
                        lvi.SubItems.Add("0: 스마트폰의 NFC 미사용 / 1: 스마트폰의 NFC 사용");
                        listView.Items.Add(lvi);
                        break;
                        */
                    case "remoteukropen":
                        lvi.SubItems.Add("웹서버 사용 여부");
                        lvi.SubItems.Add(_str[1]);
                        lvi.SubItems.Add("0: 웹서버 연동 안함  / 1: 웹서버 연동하여, 웹서버로 부터 문열림 패킷 수신");
                        listView.Items.Add(lvi);
                        break;
                     case "webip":
                        lvi.SubItems.Add("웹서버 IP");
                        lvi.SubItems.Add(_str[1]);
                        lvi.SubItems.Add("웹서버 IP");
                        listView.Items.Add(lvi);
                        break;
                    case "webport":
                        lvi.SubItems.Add("웹서버 PORT");
                        lvi.SubItems.Add(_str[1]);
                        lvi.SubItems.Add("웹서버 PORT");
                        listView.Items.Add(lvi);
                        break;
                    case "hanin_parking":
                        lvi.SubItems.Add("혜인 시스템과 연동 여부");
                        lvi.SubItems.Add(_str[1]);
                        lvi.SubItems.Add("0: 연동 안함  / 1: 연동을 하여, View 테이블에 자동으로 데이터를 입력할 수 있음. 권한은 워크벤치를 이용해야함.");
                        listView.Items.Add(lvi);
                        break;
                    default:
                        break;
                }
            }

            listView.OwnerDraw = true;

            // 행 높이 조절
            ImageList imgList = new ImageList();
            imgList.ImageSize = new Size(1, 18);
            listView.SmallImageList = imgList;
        }

        public void AddListViewRow(string ColA, string COLB, string COLC)
        {
            ListViewItem NewRow = new ListViewItem(
               new string[] { ColA, COLB, COLC }, -1, Color.Empty, Color.LightSkyBlue, null);

            //Add the items to the ListView.
            listView.Items.AddRange(new ListViewItem[] { NewRow });
        }

        int nTestCnt = 0;

        private void listView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            if (e.ColumnIndex == 0)
                return;

            using (StringFormat sf = new StringFormat())
            {

                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                e.DrawBackground();
                // Draw the header text.
                using (Font headerFont =
                            new Font("돋움", 11))
                {
                    Color bgColor = Color.FromArgb(239, 249, 254);
                    e.Graphics.FillRectangle(new SolidBrush(bgColor), e.Bounds);
                    e.Graphics.DrawString(e.Header.Text, headerFont, Brushes.Black, e.Bounds, sf);

                    e.Graphics.DrawLine(new Pen(Color.FromArgb(210, 210, 210)), new Point(e.Bounds.X, 0), new Point(e.Bounds.X, e.Bounds.Height));  // Header 세로 라인

                    if(++nTestCnt < 200){
                        Console.WriteLine("e.ColumnIndex = " + e.ColumnIndex.ToString());
                        Console.WriteLine("e.Bounds.X = " + e.Bounds.X.ToString());
                        Console.WriteLine("e.Bounds.Height = " + e.Bounds.Height.ToString());
                        
                     }
                }
            }
            return;
        }


        private void listView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void listView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true;
        }
    }
}
