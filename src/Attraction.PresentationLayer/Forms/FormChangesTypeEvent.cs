using System;
using System.Windows.Forms;
using Attraction.BusinessLayer.Interfaces;
using Attraction.BusinessLayer.Dto.TypeEvent;

namespace Attraction.PresentationLayer.Forms
{
    public partial class FormChangesTypeEvent : Form
    {
        private readonly FormMain _formMain;
        private readonly TypeEventDto _typeEventDto;
        private readonly ITypeEventService _typeEventService;

        public FormChangesTypeEvent(FormMain formMain, TypeEventDto typeEventDto, ITypeEventService typeEventService)
        {
            _formMain = formMain;
            _typeEventDto = typeEventDto;
            _typeEventService = typeEventService;
            InitializeComponent();
        }

        private void FormChangesTypeEvent_Load(object sender, EventArgs e)
        {
            textBox1.Text = _typeEventDto.Name;
            textBox2.Text = _typeEventDto.Description;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Вы должны ввести все данные!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var typeEvent = new TypeEventDto
            {
                Id = _typeEventDto.Id,
                Name = textBox1.Text,
                Description = textBox2.Text
            };

            _typeEventService.Edit(typeEvent);
            _formMain.button5_Click(null, null);
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
