using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Attraction.PresentationLayer.Tools;
using Attraction.BusinessLayer.Interfaces;
using Attraction.BusinessLayer.Dto.Attraction;

namespace Attraction.PresentationLayer.Forms
{
    public partial class FormChangesAttraction : Form
    {
        private readonly FormMain _formMain;
        private readonly AttractionDto _attractionDto;
        private readonly IAttractionService _attractionService;
        private readonly ILocalityService _localityService;
        private readonly ITypeAttractionService _typeAttractionService;

        public FormChangesAttraction(FormMain formMain, AttractionDto attractionDto,
            IAttractionService attractionService, ITypeAttractionService typeAttractionService, ILocalityService localityService)
        {
            _formMain = formMain;
            _attractionDto = attractionDto;
            _attractionService = attractionService;
            _localityService = localityService;
            _typeAttractionService = typeAttractionService;
            InitializeComponent();
        }

        private void FormChangesAttraction_Load(object sender, EventArgs e)
        {
            this.FillCombobox(comboBox1, _typeAttractionService.GetAll().Select(x => x.Name).ToArray());
            this.FillCombobox(comboBox2, _localityService.GetAll().Select(x => x.Name).ToArray());
            comboBox1.SelectedItem = _typeAttractionService.GetById(_attractionDto.TypeAttractionId).Name;
            comboBox2.SelectedItem = _localityService.GetById(_attractionDto.LocalityId).Name;
            textBox1.Text = _attractionDto.Name;
            dateTimePicker1.Value = _attractionDto.FoundationDate;
            textBox2.Text = _attractionDto.Description;
            checkBox1.Checked = _attractionDto.IsRoundСlock;
            dateTimePicker2.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                _attractionDto.StartTime.Hours, _attractionDto.StartTime.Minutes, _attractionDto.StartTime.Seconds);
            dateTimePicker3.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                _attractionDto.EndTime.Hours, _attractionDto.EndTime.Minutes, _attractionDto.EndTime.Seconds);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) ||
                comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1 || _attractionDto.Image.Length <= 0)
            {
                MessageBox.Show("Вы должны ввести все данные!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            var typeAttraction = _typeAttractionService.GetByName(comboBox1.SelectedItem.ToString());
            var locality = _localityService.GetByName(comboBox2.SelectedItem.ToString());
            var attraction = new AttractionDto
            {
                Id = _attractionDto.Id,
                Name = textBox1.Text,
                FoundationDate = dateTimePicker1.Value,
                Description = textBox2.Text,
                IsRoundСlock = checkBox1.Checked,
                StartTime = dateTimePicker2.Value.TimeOfDay,
                EndTime = dateTimePicker3.Value.TimeOfDay,
                Image = _attractionDto.Image,
                TypeAttractionId = typeAttraction.Id,
                LocalityId = locality.Id
            };

            _attractionService.Edit(attraction);
            _formMain.button1_Click(null, null);
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*PNG|All files (*.*)|*.*";

                if (openFileDialog.ShowDialog() != DialogResult.OK) return;
                var filePath = openFileDialog.FileName;
                var bitmap = new Bitmap(filePath);
                using (var memoryStream = new MemoryStream())
                {
                    bitmap.Save(memoryStream, bitmap.RawFormat);
                    _attractionDto.Image = memoryStream.ToArray();
                }
            }
        }

        private void EnterOnlyLetter(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) || e.KeyChar is (char)Keys.Back or(char)Keys.Delete)
            {
                e.Handled = false;
                return;
            }

            e.Handled = true;
        }
    }
}
