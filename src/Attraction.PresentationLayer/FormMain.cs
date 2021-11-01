using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using Attraction.BusinessLayer.Services;
using Attraction.BusinessLayer.Dto.Event;
using Attraction.PresentationLayer.Tools;
using Attraction.BusinessLayer.Interfaces;
using Attraction.BusinessLayer.Dto.Locality;
using Attraction.BusinessLayer.Dto.TypeEvent;
using Attraction.BusinessLayer.Dto.Attraction;
using Attraction.BusinessLayer.Dto.TypeAttraction;
using Attraction.DataAccessLayer.Repository.EntityFramework;

namespace Attraction.PresentationLayer
{
    public partial class FormMain : Form
    {
        public CurrentTable CurrentTable = CurrentTable.Attraction;
        private readonly IAttractionService _attractionService;
        private readonly IEventService _eventService;
        private readonly ILocalityService _localityService;
        private readonly ITypeAttractionService _typeAttractionService;
        private readonly ITypeEventService _typeEventService;
        private readonly List<Panel> _addPanels;

        public FormMain()
        {
            InitializeComponent();
            var databaseContext = new DatabaseContextEntityFramework();
            #region Init repositoties

            _attractionService = new AttractionService(databaseContext);
            _eventService = new EventService(databaseContext);
            _localityService = new LocalityService(databaseContext);
            _typeAttractionService = new TypeAttractionService(databaseContext);
            _typeEventService = new TypeEventService(databaseContext);

            #endregion
            _addPanels = new List<Panel> { panel3, panel5, panel6, panel7, panel8 };
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.FillCombobox(comboBox1, _typeAttractionService.GetAll().Select(x => x.Name).ToArray());
            this.FillCombobox(comboBox2, _localityService.GetAll().Select(x => x.Name).ToArray());
            this.FillCombobox(comboBox3, _attractionService.GetAll().Select(x => x.Name).ToArray());
            this.FillCombobox(comboBox4, _typeEventService.GetAll().Select(x => x.Name).ToArray());
            button1_Click(null, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CurrentTable = CurrentTable.Attraction;
            this.OpenAddPanel(panel3, _addPanels);
            this.GenerateColumns(
                listView1,
                90, 
                "Код", "Имя", "Дата основания", "Описание", "Круглосуточность", "Начало", "Конец", "Место нахожднеия", "Тип"
                );
            foreach (var item in _attractionService.GetAll())
            {
                var newItem = new ListViewItem(new []
                {
                    item.Id.ToString(),
                    item.Name,
                    item.FoundationDate.ToShortDateString(),
                    item.Description,
                    item.IsRoundСlock ? "Да" : "Нет",
                    item.StartTime.ToString(),
                    item.EndTime.ToString(),
                    item.LocalityDto.Name,
                    item.TypeAttractionDto.Name,
                });
                listView1.Items.Add(newItem);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CurrentTable = CurrentTable.Event;
            this.OpenAddPanel(panel5, _addPanels);
            this.GenerateColumns(
                listView1,
                120,
                "Код", "Имя", "Дата", "Начало", "Описание", "Тип", "Достопримечательность"
            );
            foreach (var item in _eventService.GetAll())
            {
                var newItem = new ListViewItem(new[]
                {
                    item.Id.ToString(),
                    item.Name,
                    item.Date.ToShortDateString(),
                    item.StartTime.ToString(),
                    item.Description,
                    item.TypeEventDto.Name,
                    item.AttractionDto.Name
                });
                listView1.Items.Add(newItem);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CurrentTable = CurrentTable.Locality;
            this.OpenAddPanel(panel6, _addPanels);
            this.GenerateColumns(
                listView1,
                135,
                "Код", "Имя", "Регион", "Адрес", "Широта", "Долгота"
            );
            foreach (var item in _localityService.GetAll())
            {
                var newItem = new ListViewItem(new[]
                {
                    item.Id.ToString(),
                    item.Name,
                    item.Region,
                    item.Address,
                    item.Latitude,
                    item.Longitude
                });
                listView1.Items.Add(newItem);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CurrentTable = CurrentTable.TypeAttraction;
            this.OpenAddPanel(panel7, _addPanels);
            this.GenerateColumns(
                listView1,
                200,
                "Код", "Имя", "Описание"
            );
            foreach (var item in _typeAttractionService.GetAll())
            {
                var newItem = new ListViewItem(new[]
                {
                    item.Id.ToString(),
                    item.Name,
                    item.Description
                });
                listView1.Items.Add(newItem);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CurrentTable = CurrentTable.TypeEvent;
            this.OpenAddPanel(panel8, _addPanels);
            this.GenerateColumns(
                listView1,
                200,
                "Код", "Имя", "Описание"
            );
            foreach (var item in _typeEventService.GetAll())
            {
                var newItem = new ListViewItem(new[]
                {
                    item.Id.ToString(),
                    item.Name,
                    item.Description
                });
                listView1.Items.Add(newItem);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var typeAttraction = _typeAttractionService.GetByName(comboBox1.SelectedItem.ToString());
            var locality = _localityService.GetByName(comboBox2.SelectedItem.ToString());
            var attraction = new AttractionDto
            {
                Name = textBox1.Text,
                FoundationDate = dateTimePicker1.Value,
                Description = textBox2.Text,
                IsRoundСlock = checkBox1.Checked,
                StartTime = dateTimePicker2.Value.TimeOfDay,
                EndTime = dateTimePicker3.Value.TimeOfDay,
                TypeAttractionId = typeAttraction.Id,
                LocalityId = locality.Id
            };

            _attractionService.Create(attraction);
            button1_Click(this, null);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var typeEvent = _typeEventService.GetByName(comboBox4.SelectedItem.ToString());
            var attraction = _attractionService.GetByName(comboBox3.SelectedItem.ToString());
            var eEvent = new EventDto
            {
                Name = textBox4.Text,
                Date = dateTimePicker6.Value,
                Description = textBox3.Text,
                StartTime = dateTimePicker5.Value.TimeOfDay,
                TypeEventId = typeEvent.Id,
                AttractionId = attraction.Id
            };

            _eventService.Create(eEvent);
            button2_Click(this, null);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var locality = new LocalityDto
            {
                Name = textBox6.Text,
                Region = comboBox6.SelectedItem.ToString(),
                Address = textBox5.Text,
                Latitude = textBox7.Text,
                Longitude = textBox8.Text
            };

            _localityService.Create(locality);
            button3_Click(this, null);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var typeAttraction = new TypeAttractionDto
            {
                Name = textBox10.Text,
                Description = textBox9.Text
            };

            _typeAttractionService.Create(typeAttraction);
            button4_Click(this, null);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var typeEvent = new TypeEventDto
            {
                Name = textBox12.Text,
                Description = textBox11.Text
            };

            _typeEventService.Create(typeEvent);
            button5_Click(this, null);
        }

        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Выберите строку таблицы!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            switch (CurrentTable)
            {
                case CurrentTable.Attraction:
                    _attractionService.Delete(id);
                    button1_Click(this, null);
                    break;
                case CurrentTable.Event:
                    _eventService.Delete(id);
                    button2_Click(this, null);
                    break;
                case CurrentTable.Locality:
                    _localityService.Delete(id);
                    button3_Click(this, null);
                    break;
                case CurrentTable.TypeAttraction:
                    _typeAttractionService.Delete(id);
                    button4_Click(this, null);
                    break;
                case CurrentTable.TypeEvent:
                    _typeEventService.Delete(id);
                    button5_Click(this, null);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Выберите строку таблицы!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            switch (CurrentTable)
            {
                case CurrentTable.Attraction:
                    var target1 = _attractionService.GetById(id);
                    break;
                case CurrentTable.Event:
                    var target2 = _eventService.GetById(id);
                    break;
                case CurrentTable.Locality:
                    var target3 = _localityService.GetById(id);
                    break;
                case CurrentTable.TypeAttraction:
                    var target4 = _typeAttractionService.GetById(id);
                    break;
                case CurrentTable.TypeEvent:
                    var target5 = _typeEventService.GetById(id);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
