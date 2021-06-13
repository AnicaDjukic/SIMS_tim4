using Bolnica.Model.Pregledi;
using Model.Korisnici;
using Model.Pacijenti;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Bolnica.DTO
{
    public class PacijentDTO
    {
        public Pacijent Pacijent { get; set; }
        public List<TextBox> textBoxes { get; set; }
        public DatePicker datePicker { get; set; }
        public ComboBox comboBox { get; set; }
        public CheckBox checkBox { get; set; }
        public RadioButton radioButton { get; set; }
        public List<Sastojak> Alergeni { get; set; }

        public PacijentDTO() 
        {
        }

        public PacijentDTO(Pacijent p)
        {
            this.Pacijent = p;
        }

        public PacijentDTO(List<TextBox> textBoxes, DatePicker datePicker, ComboBox comboBox, CheckBox checkBox, RadioButton radioButton, List<Sastojak> alergeni) 
        {
            this.textBoxes = textBoxes;
            this.datePicker = datePicker;
            this.comboBox = comboBox;
            this.checkBox = checkBox;
            this.radioButton = radioButton;
            this.Alergeni = alergeni;
        }
    }
}
