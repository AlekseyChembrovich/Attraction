using System;
using System.Windows.Forms;
using Attraction.BusinessLayer.Interfaces;
using Attraction.BusinessLayer.Dto.TypeAttraction;

namespace Attraction.PresentationLayer.Forms
{
    public partial class FormChangesTypeAttraction : Form
    {
        private readonly FormMain _formMain;
        private readonly TypeAttractionDto _typeAttractionDto;
        private readonly ITypeAttractionService _typeAttractionService;

        public FormChangesTypeAttraction(FormMain formMain, TypeAttractionDto typeAttractionDto, ITypeAttractionService typeAttractionService)
        {
            _formMain = formMain;
            _typeAttractionDto = typeAttractionDto;
            _typeAttractionService = typeAttractionService;
            InitializeComponent();
        }

        private void FormChangesTypeAttraction_Load(object sender, EventArgs e)
        {
            textBox1.Text = _typeAttractionDto.Name;
            textBox2.Text = _typeAttractionDto.Description;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Вы должны ввести все данные!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var typeAttraction = new TypeAttractionDto
            {
                Id = _typeAttractionDto.Id,
                Name = textBox1.Text,
                Description = textBox2.Text
            };

            _typeAttractionService.Edit(typeAttraction);
            _formMain.button4_Click(null, null);
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
    }
}
