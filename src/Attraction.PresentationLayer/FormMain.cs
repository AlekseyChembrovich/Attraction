using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using Attraction.BusinessLayer.Services;
using Attraction.BusinessLayer.Dto.Event;
using Attraction.PresentationLayer.Tools;
using Attraction.PresentationLayer.Forms;
using Attraction.BusinessLayer.Interfaces;
using Word = Microsoft.Office.Interop.Word;
using Attraction.BusinessLayer.Dto.Locality;
using Attraction.BusinessLayer.Dto.TypeEvent;
using Excel = Microsoft.Office.Interop.Excel;
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
        private readonly List<Button> _tablesButtons;

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
            _tablesButtons = new List<Button> {button1, button2, button3, button4, button5};
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            UpdateCombobox();
            button1_Click(null, null);
        }

        private void UpdateCombobox()
        {
            this.FillCombobox(comboBox1, _typeAttractionService.GetAll().Select(x => x.Name).ToArray());
            this.FillCombobox(comboBox5, _attractionService.GetAll().Select(x => x.Name).ToArray());
            this.FillCombobox(comboBox2, _localityService.GetAll().Select(x => x.Name).ToArray());
            this.FillCombobox(comboBox3, _attractionService.GetAll().Select(x => x.Name).ToArray());
            this.FillCombobox(comboBox4, _typeEventService.GetAll().Select(x => x.Name).ToArray());
        }

        private void PickOutButtonCurrentTable(CurrentTable currentTable)
        {
            var pickOut = Color.FromArgb(46, 51, 66);
            var normal = Color.FromArgb(59, 64, 71);
            var index = (int)currentTable;
            _tablesButtons[index].BackColor = pickOut;
            _tablesButtons.Except(new [] { _tablesButtons[index] }).ToList().ForEach(x =>
            {
                x.BackColor = normal;
            });
        }

        private void ToggleItemsTableContextMenu(CurrentTable currentTable)
        {
            switch (currentTable)
            {
                case CurrentTable.Attraction:
                    деталиToolStripMenuItem.Visible = true;
                    отобратьСобытияToolStripMenuItem.Visible = true;
                    отобратьДостопримечательностиToolStripMenuItem.Visible = false;
                    break;
                case CurrentTable.Locality:
                    отобратьДостопримечательностиToolStripMenuItem.Visible = true;
                    деталиToolStripMenuItem.Visible = false;
                    отобратьСобытияToolStripMenuItem.Visible = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(currentTable), currentTable, null);
            }
        }

        private void PrintEvents(IEnumerable<EventDto> eventsDto)
        {
            CurrentTable = CurrentTable.Event;
            PickOutButtonCurrentTable(CurrentTable);
            this.OpenAddPanel(panel5, _addPanels);
            this.GenerateColumns(
                listView1,
                120,
                "Код", "Имя", "Дата", "Начало", "Описание", "Тип", "Достопримечательность"
            );
            foreach (var item in eventsDto)
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

            ToggleItemsTableContextMenu(CurrentTable);
        }

        public void button1_Click(object sender, EventArgs e)
        {
            CurrentTable = CurrentTable.Attraction;
            PickOutButtonCurrentTable(CurrentTable);
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

            ToggleItemsTableContextMenu(CurrentTable);
            panel10.Visible = false;
            UpdateCombobox();
        }

        public void button2_Click(object sender, EventArgs e)
        {
            var item = _eventService.GetAll();
            PrintEvents(item);
            panel10.Visible = true;
            UpdateCombobox();
        }

        public void button3_Click(object sender, EventArgs e)
        {
            CurrentTable = CurrentTable.Locality;
            PickOutButtonCurrentTable(CurrentTable);
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

            ToggleItemsTableContextMenu(CurrentTable);
            panel10.Visible = false;
            UpdateCombobox();
        }

        public void button4_Click(object sender, EventArgs e)
        {
            CurrentTable = CurrentTable.TypeAttraction;
            PickOutButtonCurrentTable(CurrentTable);
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

            ToggleItemsTableContextMenu(CurrentTable);
            panel10.Visible = false;
            UpdateCombobox();
        }

        public void button5_Click(object sender, EventArgs e)
        {
            CurrentTable = CurrentTable.TypeEvent;
            PickOutButtonCurrentTable(CurrentTable);
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

            ToggleItemsTableContextMenu(CurrentTable);
            panel10.Visible = false;
            UpdateCombobox();
        }

        private void button6_Click(object sender, EventArgs e) => Application.Exit();

        private void button8_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) ||
                comboBox1.SelectedIndex == -1 || comboBox2.SelectedIndex == -1 || _image.Length <= 0)
            {
                MessageBox.Show("Вы должны ввести все данные!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
                Image = _image,
                TypeAttractionId = typeAttraction.Id,
                LocalityId = locality.Id
            };

            _attractionService.Create(attraction);
            button1_Click(this, null);
            ClearAttractionAddPanel();
        }

        private void ClearAttractionAddPanel()
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            dateTimePicker3.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            checkBox1.Checked = false;
            _image = Array.Empty<byte>();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox4.Text) || string.IsNullOrWhiteSpace(textBox3.Text) ||
                comboBox4.SelectedIndex == -1 || comboBox3.SelectedIndex == -1)
            {
                MessageBox.Show("Вы должны ввести все данные!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
            ClearEventAddPanel();
        }

        private void ClearEventAddPanel()
        {
            textBox3.Text = string.Empty;
            textBox4.Text = string.Empty;
            dateTimePicker6.Value = DateTime.Now;
            dateTimePicker5.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox6.Text) || string.IsNullOrWhiteSpace(textBox5.Text) || comboBox6.SelectedIndex == -1)
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
                Name = textBox6.Text,
                Region = comboBox6.SelectedItem.ToString(),
                Address = textBox5.Text,
                Latitude = maskedTextBox1.Text,
                Longitude = maskedTextBox1.Text
            };

            _localityService.Create(locality);
            button3_Click(this, null);
            ClearLocationAddPanel();
        }

        private void ClearLocationAddPanel()
        {
            textBox6.Text = string.Empty;
            textBox5.Text = string.Empty;
            comboBox6.SelectedIndex = -1;
            maskedTextBox1.Text = string.Empty;
            maskedTextBox2.Text = string.Empty;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox10.Text) || string.IsNullOrWhiteSpace(textBox9.Text))
            {
                MessageBox.Show("Вы должны ввести все данные!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var typeAttraction = new TypeAttractionDto
            {
                Name = textBox10.Text,
                Description = textBox9.Text
            };

            _typeAttractionService.Create(typeAttraction);
            button4_Click(this, null);
            ClearTypeAttractionPanel();
        }

        private void ClearTypeAttractionPanel()
        {
            textBox9.Text = string.Empty;
            textBox10.Text = string.Empty;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox12.Text) || string.IsNullOrWhiteSpace(textBox11.Text))
            {
                MessageBox.Show("Вы должны ввести все данные!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var typeEvent = new TypeEventDto
            {
                Name = textBox12.Text,
                Description = textBox11.Text
            };

            _typeEventService.Create(typeEvent);
            button5_Click(this, null);
            ClearTypeEventPanel();
        }

        private void ClearTypeEventPanel()
        {
            textBox11.Text = string.Empty;
            textBox12.Text = string.Empty;
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
                    var attractionDto = _attractionService.GetById(id);
                    var formChangesAttraction = new FormChangesAttraction(this, attractionDto, _attractionService, _typeAttractionService, _localityService);
                    formChangesAttraction.ShowDialog();
                    break;
                case CurrentTable.Event:
                    var eventDto = _eventService.GetById(id);
                    var formChangesEvent = new FormChangesEvent(this, eventDto, _attractionService, _typeEventService, _eventService);
                    formChangesEvent.ShowDialog();
                    break;
                case CurrentTable.Locality:
                    var localityDto = _localityService.GetById(id);
                    var formChangesLocality = new FormChangesLocality(this, localityDto, _localityService);
                    formChangesLocality.ShowDialog();
                    break;
                case CurrentTable.TypeAttraction:
                    var typeAttractionDto = _typeAttractionService.GetById(id);
                    var formChangesTypeAttraction = new FormChangesTypeAttraction(this, typeAttractionDto, _typeAttractionService);
                    formChangesTypeAttraction.ShowDialog();
                    break;
                case CurrentTable.TypeEvent:
                    var typeEvent = _typeEventService.GetById(id);
                    var formChangesTypeEvent = new FormChangesTypeEvent(this, typeEvent, _typeEventService);
                    formChangesTypeEvent.ShowDialog();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void деталиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Выберите строку таблицы!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var attractionId = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            var attractionDto = _attractionService.GetById(attractionId);
            var formDetailsAttraction =
                new FormDetailsAttraction(attractionDto, _typeAttractionService, _localityService);
            formDetailsAttraction.ShowDialog();
        }

        private void отобратьСобытияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Выберите строку таблицы!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var attractionId = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            var eventsDto = _eventService.GetAll().ToList();
            var eventsFilter = eventsDto.Where(x => x.AttractionId == attractionId).ToList();
            PrintEvents(eventsFilter);
        }

        private void button13_Click(object sender, EventArgs e) => ClearAttractionAddPanel();

        private void button14_Click(object sender, EventArgs e) => ClearEventAddPanel();

        private void button15_Click(object sender, EventArgs e) => ClearLocationAddPanel();

        private void button16_Click(object sender, EventArgs e) => ClearTypeAttractionPanel();

        private void button17_Click(object sender, EventArgs e) => ClearTypeEventPanel();

        private byte[] _image = Array.Empty<byte>();

        private void button18_Click(object sender, EventArgs e)
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
                    _image = memoryStream.ToArray();
                }
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            var eventsDto = _eventService.GetAll().ToList();
            eventsDto = eventsDto.Where(x => x.Date == dateTimePicker4.Value.Date).ToList();

            if (comboBox5.SelectedIndex != -1)
            {
                var nameAttraction = comboBox5.SelectedItem.ToString();
                var attractionDto = _attractionService.GetByName(nameAttraction);
                eventsDto = eventsDto.Where(x => x.AttractionId == attractionDto.Id).ToList();
            }

            PrintEvents(eventsDto);
        }

        private void button20_Click(object sender, EventArgs e)
        {
            dateTimePicker4.Value = DateTime.Now;
            comboBox5.SelectedIndex = -1;
            button2_Click(null, null);
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

        private void EnterLetterAndDigit(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetterOrDigit(e.KeyChar) || e.KeyChar is (char)Keys.Back or (char)Keys.Delete)
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

        private void wordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentTable != CurrentTable.Event)
            {
                MessageBox.Show("Выберите таблицу событий!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (listView1.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Выберите строку таблицы!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var eventId = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            var eventDto = _eventService.GetById(eventId);
            var typeEventDto = _typeEventService.GetById(eventDto.TypeEventId);
            var attractionDto = _attractionService.GetById(eventDto.AttractionId);

            var application = new Word.Application
            {
                Visible = false
            };
            var path = Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Documents\\Attraction.docx");
            var document = application.Documents.Open(path);
            ReplaceData("{name}", eventDto.Name, document);
            ReplaceData("{date}", eventDto.Date.ToShortDateString(), document);
            ReplaceData("{time}", eventDto.StartTime.ToString("g"), document);
            ReplaceData("{description}", eventDto.Description, document);
            ReplaceData("{type}", typeEventDto.Name, document);
            ReplaceData("{attraction}", attractionDto.Name, document);
            document.SaveAs(Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\Documents\\Result.docx"));
            application.Visible = true;

            void ReplaceData(string target, string data, Word.Document documentMy)
            {
                var content = documentMy.Content;
                content.Find.ClearFormatting();
                content.Find.Execute(FindText: target, ReplaceWith: data);
            }
        }
        
        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            var itemsListView = listView1.Items;
            if (textBox7.Text == string.Empty)
            {
                foreach (ListViewItem item in itemsListView)
                {
                    item.Selected = false;
                }
                return;
            }

            foreach (ListViewItem item in itemsListView)
            {
                for (var i = 0; i < item.SubItems.Count; i++)
                {
                    if (item.SubItems[i].Text.ToLower().Contains(textBox7.Text.ToLower()))
                    {
                        item.Selected = true;
                        break;
                    }

                    item.Selected = false;
                }
            }
        }

        private void PrintIntoExcel(CurrentTable currentTable, params string[] namesColumns)
        {
            var application = new Excel.Application();
            var worksheet = (Excel.Worksheet)application.Workbooks.Add(Type.Missing).ActiveSheet;
            const int indexFirstLetter = 65;
            var nextLetter = Convert.ToChar(indexFirstLetter + namesColumns.Length - 1);
            var excelCells = worksheet.get_Range("A1", $"{nextLetter}1").Cells;
            excelCells.HorizontalAlignment = Excel.Constants.xlCenter;
            excelCells.Interior.Color = Color.Gold;
            excelCells.Merge(Type.Missing);
            var nameTable = currentTable switch
            {
                CurrentTable.Attraction => "Достопримечательности",
                CurrentTable.Event => "События",
                CurrentTable.Locality => "Места нахождения",
                CurrentTable.TypeAttraction => "Тыпи достопримечательностей",
                CurrentTable.TypeEvent => "Типы событий",
                _ => throw new ArgumentOutOfRangeException()
            };

            worksheet.Cells[1, 1] = $"Табличны данные \"{nameTable}\"";
            for (var i = 0; i < namesColumns.Length; i++)
            {
                worksheet.Cells[2, i + 1] = namesColumns[i];
                worksheet.Cells[2, i + 1].HorizontalAlignment = Excel.Constants.xlCenter;
                worksheet.Columns[i + 1].ColumnWidth = 40;
            }

            switch (currentTable)
            {
                case CurrentTable.Attraction:
                    var attractionsDto = _attractionService.GetAll();
                    var listAttractions = attractionsDto.ToList();
                    for (var i = 0; i < listAttractions.Count; i++)
                    {
                        application.Cells[i + 3, 1] = listAttractions[i].Name;
                        application.Cells[i + 3, 2] = listAttractions[i].FoundationDate.Date;
                        application.Cells[i + 3, 3] = listAttractions[i].Description;
                        application.Cells[i + 3, 4] = listAttractions[i].IsRoundСlock ? "Да" : "Нет";
                        application.Cells[i + 3, 5] = listAttractions[i].StartTime.ToString("g");
                        application.Cells[i + 3, 6] = listAttractions[i].EndTime.ToString("g");
                        var locality = _localityService.GetById(listAttractions[i].LocalityId);
                        var typeAttraction = _typeAttractionService.GetById(listAttractions[i].TypeAttractionId);
                        application.Cells[i + 3, 7] = locality.Name;
                        application.Cells[i + 3, 8] = typeAttraction.Name;
                    }
                    break;
                case CurrentTable.Event:
                    var eventsDto = _eventService.GetAll();
                    var listEvents = eventsDto.ToList();
                    for (var i = 0; i < listEvents.Count; i++)
                    {
                        application.Cells[i + 3, 1] = listEvents[i].Name;
                        application.Cells[i + 3, 2] = listEvents[i].Date.Date;
                        application.Cells[i + 3, 3] = listEvents[i].StartTime.ToString("g");
                        application.Cells[i + 3, 4] = listEvents[i].Description;
                        var attraction = _attractionService.GetById(listEvents[i].AttractionId);
                        var typeEvent = _typeEventService.GetById(listEvents[i].TypeEventId);
                        application.Cells[i + 3, 5] = attraction.Name;
                        application.Cells[i + 3, 6] = typeEvent.Name;
                    }
                    break;
                case CurrentTable.Locality:
                    var localitiesDto = _localityService.GetAll();
                    var listLocalities = localitiesDto.ToList();
                    for (var i = 0; i < listLocalities.Count; i++)
                    {
                        application.Cells[i + 3, 1] = listLocalities[i].Region;
                        application.Cells[i + 3, 2] = listLocalities[i].Address;
                        application.Cells[i + 3, 3] = listLocalities[i].Latitude;
                        application.Cells[i + 3, 4] = listLocalities[i].Longitude;
                    }
                    break;
                case CurrentTable.TypeAttraction:
                    var typesAttractionDto = _typeAttractionService.GetAll();
                    var listTypesAttraction = typesAttractionDto.ToList();
                    for (var i = 0; i < listTypesAttraction.Count; i++)
                    {
                        application.Cells[i + 3, 1] = listTypesAttraction[i].Name;
                        application.Cells[i + 3, 2] = listTypesAttraction[i].Description;
                    }
                    break;
                case CurrentTable.TypeEvent:
                    var typesEvent = _typeEventService.GetAll();
                    var listTypesEvent = typesEvent.ToList();
                    for (var i = 0; i < listTypesEvent.Count; i++)
                    {
                        application.Cells[i + 3, 1] = listTypesEvent[i].Name;
                        application.Cells[i + 3, 2] = listTypesEvent[i].Description;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            application.Visible = true;
        }

        private void достопримечательностиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintIntoExcel(CurrentTable.Attraction, "Название", "Дата основания", "Описание", "Круглосуточность",
                "Время начала", "Время окончания", "Местоположение", "Тип достопримечательности");
        }

        private void событияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintIntoExcel(CurrentTable.Event, "Название", "Дата проведения", "Время начала", "Описание",
                "Достопримечательность", "Тип сотбытия");
        }

        private void местаНахожденияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintIntoExcel(CurrentTable.TypeEvent, "Регион", "Адрес", "Широта", "Долгота");
        }

        private void типыДостопримечательностейToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintIntoExcel(CurrentTable.TypeAttraction, "Название", "Описание");
        }

        private void типыСобытийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintIntoExcel(CurrentTable.TypeEvent, "Название", "Описание");
        }

        private void отобратьДостопримечательностиToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
