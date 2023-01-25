using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Lab3
{
    public partial class Form1 : Form
    { // Использую этот случайный объект, чтобы выбрать случайные значки для квадратов
        Random random = new Random();
        Label firstClicked = null;

        // Второй щелчок указывает на второй элемент управления меткой
        // на который нажимает игрок
        Label secondClicked = null;

        // Каждая из этих букв представляет собой интересный значок
        // в шрифте Webdings,
        // и каждый значок появляется дважды в этом списке
        List<string> icons = new List<string>()
    {
        "!", "!", "N", "N", ",", ",", "k", "k",
        "b", "b", "v", "v", "w", "w", "z", "z"
    };
        private void AssignIconsToSquares()
        {
            // Панель TableLayoutPanel имеет 16 меток,
            // а список значков содержит 16 значков,
            // поэтому значок выбирается случайным образом из списка
            // и добавляется к каждой метке
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // Если выбранная метка черная, игрок нажал
                // значок, который уже был показан 
                // игнорировать щелчок
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;

                    return;

                }
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                CheckForWinner();


                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                // Если игрок зайдет так далеко, игрок 
                // нажал на два разных значка, поэтому запустите
                // таймер (который подождет три четверти
                // секунды, а затем скроет значки
                timer1.Start();
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            {
                // Остановить таймер
                timer1.Stop();

                // Скрыть оба значка
                firstClicked.ForeColor = firstClicked.BackColor;
                secondClicked.ForeColor = secondClicked.BackColor;



                // Сброс первого и второго щелчков
                // таким образом, при следующем нажатии на метку
                // программа знает, что это первый щелчок
                firstClicked = null;
                secondClicked = null;



            }


        }
        private void CheckForWinner()
        {
            // Просмотрите все метки в TableLayoutPanel,
            // проверяя каждую из них, чтобы увидеть, соответствует ли ее значок
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            // Если цикл не возвращался, он не нашел
            // никаких несопоставимых значков
            // Это означает, что пользователь выиграл. Покажите сообщение и закройте форму
            MessageBox.Show("You matched all the icons!", "Congratulations");
            
        }

        private void изменитьМветМенюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            menuStrip1.BackColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
        }

        private void новаяИграToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart(); // запускает программу заново 
            
        }

        private void закрытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
           this.Close();
        }
    }

}
