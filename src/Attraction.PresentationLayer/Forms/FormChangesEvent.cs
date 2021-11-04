using System;
using System.Linq;
using System.Windows.Forms;
using Attraction.BusinessLayer.Dto.Event;
using Attraction.PresentationLayer.Tools;
using Attraction.BusinessLayer.Interfaces;

namespace Attraction.PresentationLayer.Forms
{
    public partial class FormChangesEvent : Form
    {
        private readonly FormMain _formMain;
        private readonly EventDto _eventDto;
        private readonly IAttractionService _attractionService;
        private readonly ITypeEventService _typeEventService;
        private readonly IEventService _eventService;

        public FormChangesEvent(FormMain formMain, EventDto eventDto,
            IAttractionService attractionService, ITypeEventService typeEventService, IEventService eventService)
        {
            _formMain = formMain;
            _eventDto = eventDto;
            _attractionService = attractionService;
            _typeEventService = typeEventService;
            _eventService = eventService;
            InitializeComponent();
        }

        private void FormChangesEvent_Load(object sender, EventArgs e)
        {
            this.FillCombobox(comboBox1, _typeEventService.GetAll().Select(x => x.Name).ToArray());
            this.FillCombobox(comboBox2, _attractionService.GetAll().Select(x => x.Name).ToArray());
            comboBox1.SelectedItem = _typeEventService.GetById(_eventDto.TypeEventId).Name;
            comboBox2.SelectedItem = _attractionService.GetById(_eventDto.AttractionId).Name;
            textBox1.Text = _eventDto.Name;
            dateTimePicker1.Value = _eventDto.Date;
            textBox2.Text = _eventDto.Description;
            dateTimePicker2.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                _eventDto.StartTime.Hours, _eventDto.StartTime.Minutes, _eventDto.StartTime.Seconds);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) ||
                comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("Вы должны ввести все данные!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var typeEvent = _typeEventService.GetByName(comboBox1.SelectedItem.ToString());
            var attraction = _attractionService.GetByName(comboBox2.SelectedItem.ToString());
            var eEvent = new EventDto
            {
                Id = _eventDto.Id,
                Name = textBox1.Text,
                Date = dateTimePicker1.Value,
                Description = textBox2.Text,
                StartTime = dateTimePicker2.Value.TimeOfDay,
                TypeEventId = typeEvent.Id,
                AttractionId = attraction.Id
            };

            _eventService.Edit(eEvent);
            _formMain.button2_Click(null, null);
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
