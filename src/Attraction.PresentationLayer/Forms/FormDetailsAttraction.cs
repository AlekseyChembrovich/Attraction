using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using Attraction.BusinessLayer.Interfaces;
using Attraction.BusinessLayer.Dto.Attraction;

namespace Attraction.PresentationLayer.Forms
{
    public partial class FormDetailsAttraction : Form
    {
        private readonly AttractionDto _attractionDto;
        private readonly ILocalityService _localityService;
        private readonly ITypeAttractionService _typeAttractionService;

        public FormDetailsAttraction(AttractionDto attractionDto, ITypeAttractionService typeAttractionService, ILocalityService localityService)
        {
            _attractionDto = attractionDto;
            _localityService = localityService;
            _typeAttractionService = typeAttractionService;
            InitializeComponent();
        }

        private void FormDetailsAttraction_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            label1.Text += _attractionDto.Name;
            label2.Text += _attractionDto.FoundationDate.ToShortDateString();
            label3.Text += _attractionDto.IsRoundСlock ? "Да" : "Нет";
            label4.Text += _attractionDto.StartTime.ToString("g");
            label5.Text += _attractionDto.EndTime.ToString("g");
            var typeAttractionDto = _typeAttractionService.GetById(_attractionDto.TypeAttractionId);
            var localityDto = _localityService.GetById(_attractionDto.LocalityId);
            label6.Text += typeAttractionDto.Name;
            label7.Text += localityDto.Name;
            textBox1.Text = _attractionDto.Description;

            if (_attractionDto.Image == null) return;
            if (_attractionDto.Image.Length <= 0) return;
            Bitmap bitmap;
            using (var memoryStream = new MemoryStream(_attractionDto.Image))
            {
                bitmap = new Bitmap(memoryStream);
            }

            pictureBox1.Image = bitmap;
        }
    }
}
