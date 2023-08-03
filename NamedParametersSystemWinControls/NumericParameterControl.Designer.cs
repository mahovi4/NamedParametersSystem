namespace NamedParametersSystemWinControls
{
    partial class NumericParameterControl
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
            this.nudValue = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudValue)).BeginInit();
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
            // nudValue
            // 
            this.nudValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nudValue.Location = new System.Drawing.Point(42, 0);
            this.nudValue.Name = "nudValue";
            this.nudValue.Size = new System.Drawing.Size(100, 23);
            this.nudValue.TabIndex = 1;
            this.nudValue.ValueChanged += new System.EventHandler(this.nudValue_ValueChanged);
            // 
            // NumericParameterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nudValue);
            this.Controls.Add(this.lName);
            this.MinimumSize = new System.Drawing.Size(142, 23);
            this.Name = "NumericParameterControl";
            this.Size = new System.Drawing.Size(142, 23);
            ((System.ComponentModel.ISupportInitialize)(this.nudValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolTip ttMain;
        private Label lName;
        private NumericUpDown nudValue;
    }
}
