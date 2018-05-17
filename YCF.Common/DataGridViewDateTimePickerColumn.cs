using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace Common
{
    public class DataGridViewDateTimePickerColumn : DataGridViewColumn
    {
        private DateTimePickerFormat _format = DateTimePickerFormat.Long;
        private string _customFormat;
        private bool _showUpDown;

        public DataGridViewDateTimePickerColumn()
            : base(new DataGridViewDateTimePickerCell())
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                // Ensure that the cell used for the template is a CalendarCell.
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(DataGridViewDateTimePickerCell)))
                {
                    throw new InvalidCastException("Must be a DataGridViewDateTimePickerCell");
                }
                base.CellTemplate = value;
            }
        }

        public override object Clone()
        {
            DataGridViewDateTimePickerColumn colClone = base.Clone() as DataGridViewDateTimePickerColumn;
            colClone.Format = _format;
            colClone.CustomFormat = _customFormat;
            colClone.ShowUpDown = _showUpDown;
            return colClone;
        }

        public DateTimePickerFormat Format
        {
            get
            {
                return _format;
            }
            set
            {
                _format = value;
            }
        }

        public string CustomFormat
        {
            get
            {
                return _customFormat;
            }
            set
            {
                _customFormat = value;
            }
        }

        public bool ShowUpDown
        {
            get
            {
                return _showUpDown;
            }
            set
            {
                _showUpDown = value;
            }
        }
    }

    public class DataGridViewDateTimePickerCell : DataGridViewTextBoxCell
    {

        public DataGridViewDateTimePickerCell()
            : base()
        {
            // Use the short date format.
            //this.Style.Format = "d";
        }

        public override void InitializeEditingControl(int rowIndex, object
            initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            // Set the value of the editing control to the current cell value.
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);
            DataGridViewDateTimePickerEditingControl ctl =
                DataGridView.EditingControl as DataGridViewDateTimePickerEditingControl;
            try
            {
                ctl.Value = (DateTime)this.Value;
            }
            catch (ArgumentOutOfRangeException oorEx)
            {
                ctl.Value = System.DateTime.Now;
            }
            catch (NullReferenceException nullEx)
            {
                ctl.Value = System.DateTime.Now;
            }

            DataGridViewDateTimePickerColumn owningCol = this.OwningColumn as DataGridViewDateTimePickerColumn;
            if (owningCol != null)
            {
                ctl.Format = owningCol.Format;
                ctl.CustomFormat = owningCol.CustomFormat;
                ctl.ShowUpDown = owningCol.ShowUpDown;
            }
        }

        protected override object GetFormattedValue(object value, int rowIndex, ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter, TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            // 设置显示的时间和日期格式
            if (this.RowIndex < 0 || this.DataGridView.Rows[this.RowIndex].IsNewRow)
            {
                return string.Empty;
            }

            DataGridViewDateTimePickerColumn owningCol = this.OwningColumn as DataGridViewDateTimePickerColumn;
            if (owningCol != null)
            {


                switch (owningCol.Format)
                {
                    case DateTimePickerFormat.Custom:
                        this.Style.Format = owningCol.CustomFormat;
                        break;
                    case DateTimePickerFormat.Long:
                        this.Style.Format = "D";
                        break;
                    case DateTimePickerFormat.Short:
                        this.Style.Format = "d";
                        break;
                    case DateTimePickerFormat.Time:
                        this.Style.Format = "T";
                        break;
                }
            }

            return base.GetFormattedValue(value, rowIndex, ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context);
        }

        public override Type EditType
        {
            get
            {
                // Return the type of the editing contol that CalendarCell uses.
                return typeof(DataGridViewDateTimePickerEditingControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                // Return the type of the value that CalendarCell contains.
                return typeof(DateTime);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                // Use the current date and time as the default value.
                return DateTime.Now;
            }
        }
    }

    public class DataGridViewDateTimePickerEditingControl : DateTimePicker, IDataGridViewEditingControl
    {
        DataGridView dataGridView;
        private bool valueChanged = false;
        int rowIndex;

        public DataGridViewDateTimePickerEditingControl()
        {
            //this.Format = DateTimePickerFormat.Short;            
        }

        // Implements the IDataGridViewEditingControl.EditingControlFormattedValue 
        // property.
        public object EditingControlFormattedValue
        {
            get
            {
                string formattedValue = this.Value.ToString();
                //switch (this.Format)
                //{
                //    case DateTimePickerFormat.Long:
                //        formattedValue = this.Value.ToLongDateString();
                //        break;
                //    case DateTimePickerFormat.Short:
                //        formattedValue = this.Value.ToShortDateString();
                //        break;
                //    case DateTimePickerFormat.Time:
                //        formattedValue = this.Value.ToLongTimeString();
                //        break;
                //    case DateTimePickerFormat.Custom:
                //        formattedValue = this.Value.ToString(this.CustomFormat);
                //        break;                       
                //}
                return formattedValue;

                //return this.Value.ToShortDateString();
            }
            set
            {
                String newValue = value as String;
                if (newValue != null)
                {
                    this.Value = DateTime.Parse(newValue);
                }
            }
        }

        // Implements the 
        // IDataGridViewEditingControl.GetEditingControlFormattedValue method.
        public object GetEditingControlFormattedValue(
            DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }

        // Implements the 
        // IDataGridViewEditingControl.ApplyCellStyleToEditingControl method.
        public void ApplyCellStyleToEditingControl(
            DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            this.CalendarForeColor = dataGridViewCellStyle.ForeColor;
            this.CalendarMonthBackground = dataGridViewCellStyle.BackColor;
        }

        // Implements the IDataGridViewEditingControl.EditingControlRowIndex 
        // property.
        public int EditingControlRowIndex
        {
            get
            {
                return rowIndex;
            }
            set
            {
                rowIndex = value;
            }
        }

        // Implements the IDataGridViewEditingControl.EditingControlWantsInputKey 
        // method.
        public bool EditingControlWantsInputKey(
            Keys key, bool dataGridViewWantsInputKey)
        {
            // Let the DateTimePicker handle the keys listed.
            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;
                default:
                    return false;
            }
        }

        // Implements the IDataGridViewEditingControl.PrepareEditingControlForEdit 
        // method.
        public void PrepareEditingControlForEdit(bool selectAll)
        {
            // No preparation needs to be done.
        }

        // Implements the IDataGridViewEditingControl
        // .RepositionEditingControlOnValueChange property.
        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }

        // Implements the IDataGridViewEditingControl
        // .EditingControlDataGridView property.
        public DataGridView EditingControlDataGridView
        {
            get
            {
                return dataGridView;
            }
            set
            {
                dataGridView = value;
            }
        }

        // Implements the IDataGridViewEditingControl
        // .EditingControlValueChanged property.
        public bool EditingControlValueChanged
        {
            get
            {
                return valueChanged;
            }
            set
            {
                valueChanged = value;
            }
        }

        // Implements the IDataGridViewEditingControl
        // .EditingPanelCursor property.
        public Cursor EditingPanelCursor
        {
            get
            {
                return base.Cursor;
            }
        }

        protected override void OnValueChanged(EventArgs eventargs)
        {
            // Notify the DataGridView that the contents of the cell
            // have changed.
            valueChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnValueChanged(eventargs);
        }
    }

}
