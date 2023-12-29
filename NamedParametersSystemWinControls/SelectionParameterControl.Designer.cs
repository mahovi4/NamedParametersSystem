namespace NamedParametersSystemWinControls
{
    partial class SelectionParameterControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ttMain = new System.Windows.Forms.ToolTip(this.components);
            this.lName = new System.Windows.Forms.Label();
            this.cbValue = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // lName
            // 
            this.lName.AutoSize = true;
            this.lName.Location = new System.Drawing.Point(0, 4);
            this.lName.Name = "lName";
            this.lName.Size = new System.Drawing.Size(37, 15);
            this.lName.TabIndex = 0;
            this.lName.Text = "Имя: ";
            // 
            // cbValue
            // 
            this.cbValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbValue.FormattingEnabled = true;
            this.cbValue.Location = new System.Drawing.Point(42, 0);
            this.cbValue.Name = "cbValue";
            this.cbValue.Size = new System.Drawing.Size(100, 23);
            this.cbValue.TabIndex = 1;
            this.cbValue.SelectedIndexChanged += new System.EventHandler(this.cbValue_SelectedIndexChanged);
            this.cbValue.Leave += new System.EventHandler(this.cbValue_Leave);
            // 
            // SelectionParameterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbValue);
            this.Controls.Add(this.lName);
            this.MinimumSize = new System.Drawing.Size(142, 23);
            this.Name = "SelectionParameterControl";
            this.Size = new System.Drawing.Size(142, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolTip ttMain;
        private Label lName;
        private ComboBox cbValue;
    }
}
