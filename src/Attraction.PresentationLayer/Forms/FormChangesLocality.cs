using System;
using System.Windows.Forms;
using Attraction.BusinessLayer.Interfaces;
using Attraction.BusinessLayer.Dto.Locality;

namespace Attraction.PresentationLayer.Forms
{
    public partial class FormChangesLocality : Form
    {
        private readonly FormMain _formMain;
        private readonly LocalityDto _localityDto;
        private readonly ILocalityService _localityService;

        public FormChangesLocality(FormMain formMain, LocalityDto localityDto, ILocalityService localityService)
        {
            _formMain = formMain;
            _localityDto = localityDto;
            _localityService = localityService;
            InitializeComponent();
        }

        private void FormChangesLocality_Load(object sender, EventArgs e)
        {
            textBox1.Text = _localityDto.Name;
            textBox2.Text = _localityDto.Address;
            comboBox1.SelectedItem = _localityDto.Region;
            maskedTextBox1.Text = _localityDto.Latitude;
            maskedTextBox2.Text = _localityDto.Longitude;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Вы должны ввести все данные!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (maskedTextBox1.Text.Trim(' ').Length != 10 || maskedTextBox2.Text.Trim(' ').Length != 10) // 00,000000°
            {
                MessageBox.Show("Неверный ввод ширины и долготы!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var locality = new LocalityDto
            {
                Id = _localityDto.Id,
                Name = textBox1.Text,
                Region = comboBox1.SelectedItem.ToString(),
                Address = textBox2.Text,
                Latitude = maskedTextBox1.Text,
                Longitude = maskedTextBox1.Text
            };

            _localityService.Edit(locality);
            _formMain.button3_Click(null, null);
            this.Close();
        }

        private void EnterOnlyLetter(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || e.KeyChar is (char)Keys.Back or (char)Keys.Delete)
            {
                e.Handled = false;
                return;
            }

            e.Handled = true;
        }

        private void EnterAddress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || e.KeyChar is '.' or ',' or (char)Keys.Back or (char)Keys.Delete)
            {
                e.Handled = false;
                return;
            }

            e.Handled = true;
        }
    }
}
