namespace Lab5
{
    partial class Form1
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openFileDialogMap = new System.Windows.Forms.OpenFileDialog();
            this.pictureBoxMap = new System.Windows.Forms.PictureBox();
            this.listBoxRawData = new System.Windows.Forms.ListBox();
            this.btnLoadMap = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnApplyEnd = new System.Windows.Forms.Button();
            this.txtEndY = new System.Windows.Forms.TextBox();
            this.txtEndX = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnApplyStart = new System.Windows.Forms.Button();
            this.txtStartY = new System.Windows.Forms.TextBox();
            this.txtStartX = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRobotDiameter = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtRobotY = new System.Windows.Forms.TextBox();
            this.txtRobotX = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRobotCourse = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnApplyRobot = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chkShowConfigSpace = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbVisualizationStep = new System.Windows.Forms.ComboBox();
            this.btnBuildGraph = new System.Windows.Forms.Button();
            this.btnFindPath = new System.Windows.Forms.Button();
            this.chkUsePenalty = new System.Windows.Forms.CheckBox();
            this.btnSaveRoute = new System.Windows.Forms.Button();
            this.saveFileDialogCSV = new System.Windows.Forms.SaveFileDialog();
            this.chkUseConfigSpace = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMap)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialogMap
            // 
            this.openFileDialogMap.FileName = "openFileDialog1";
            this.openFileDialogMap.Filter = "Текстовые файлы|*.txt;*.csv";
            // 
            // pictureBoxMap
            // 
            this.pictureBoxMap.BackColor = System.Drawing.Color.White;
            this.pictureBoxMap.Location = new System.Drawing.Point(12, 12);
            this.pictureBoxMap.Name = "pictureBoxMap";
            this.pictureBoxMap.Size = new System.Drawing.Size(958, 573);
            this.pictureBoxMap.TabIndex = 0;
            this.pictureBoxMap.TabStop = false;
            this.pictureBoxMap.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBoxMap_MouseClick);
            // 
            // listBoxRawData
            // 
            this.listBoxRawData.FormattingEnabled = true;
            this.listBoxRawData.ItemHeight = 25;
            this.listBoxRawData.Location = new System.Drawing.Point(311, 598);
            this.listBoxRawData.Name = "listBoxRawData";
            this.listBoxRawData.Size = new System.Drawing.Size(659, 204);
            this.listBoxRawData.TabIndex = 1;
            // 
            // btnLoadMap
            // 
            this.btnLoadMap.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnLoadMap.Location = new System.Drawing.Point(12, 591);
            this.btnLoadMap.Name = "btnLoadMap";
            this.btnLoadMap.Size = new System.Drawing.Size(293, 57);
            this.btnLoadMap.TabIndex = 2;
            this.btnLoadMap.Text = "Загрузить карту";
            this.btnLoadMap.UseVisualStyleBackColor = true;
            this.btnLoadMap.Click += new System.EventHandler(this.btnLoadMap_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.DarkOrange;
            this.groupBox1.Controls.Add(this.btnApplyRobot);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtRobotCourse);
            this.groupBox1.Controls.Add(this.txtRobotY);
            this.groupBox1.Controls.Add(this.txtRobotX);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtRobotDiameter);
            this.groupBox1.Controls.Add(this.btnApplyEnd);
            this.groupBox1.Controls.Add(this.txtEndY);
            this.groupBox1.Controls.Add(this.txtEndX);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnApplyStart);
            this.groupBox1.Controls.Add(this.txtStartY);
            this.groupBox1.Controls.Add(this.txtStartX);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(977, 396);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(672, 366);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Точки старта и финиша";
            // 
            // btnApplyEnd
            // 
            this.btnApplyEnd.Location = new System.Drawing.Point(341, 109);
            this.btnApplyEnd.Name = "btnApplyEnd";
            this.btnApplyEnd.Size = new System.Drawing.Size(310, 46);
            this.btnApplyEnd.TabIndex = 9;
            this.btnApplyEnd.Text = "Установить финиш";
            this.btnApplyEnd.UseVisualStyleBackColor = true;
            this.btnApplyEnd.Click += new System.EventHandler(this.btnApplyEnd_Click);
            // 
            // txtEndY
            // 
            this.txtEndY.Location = new System.Drawing.Point(225, 114);
            this.txtEndY.Name = "txtEndY";
            this.txtEndY.Size = new System.Drawing.Size(100, 38);
            this.txtEndY.TabIndex = 8;
            // 
            // txtEndX
            // 
            this.txtEndX.Location = new System.Drawing.Point(52, 114);
            this.txtEndX.Name = "txtEndX";
            this.txtEndX.Size = new System.Drawing.Size(100, 38);
            this.txtEndX.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(179, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 31);
            this.label3.TabIndex = 6;
            this.label3.Text = "Y:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 31);
            this.label4.TabIndex = 5;
            this.label4.Text = "X:";
            // 
            // btnApplyStart
            // 
            this.btnApplyStart.Location = new System.Drawing.Point(341, 39);
            this.btnApplyStart.Name = "btnApplyStart";
            this.btnApplyStart.Size = new System.Drawing.Size(310, 46);
            this.btnApplyStart.TabIndex = 4;
            this.btnApplyStart.Text = "Установить старт";
            this.btnApplyStart.UseVisualStyleBackColor = true;
            this.btnApplyStart.Click += new System.EventHandler(this.btnApplyStart_Click);
            // 
            // txtStartY
            // 
            this.txtStartY.Location = new System.Drawing.Point(225, 44);
            this.txtStartY.Name = "txtStartY";
            this.txtStartY.Size = new System.Drawing.Size(100, 38);
            this.txtStartY.TabIndex = 3;
            // 
            // txtStartX
            // 
            this.txtStartX.Location = new System.Drawing.Point(52, 44);
            this.txtStartX.Name = "txtStartX";
            this.txtStartX.Size = new System.Drawing.Size(100, 38);
            this.txtStartX.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(179, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 31);
            this.label2.TabIndex = 1;
            this.label2.Text = "Y:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "X:";
            // 
            // txtRobotDiameter
            // 
            this.txtRobotDiameter.Location = new System.Drawing.Point(146, 205);
            this.txtRobotDiameter.Name = "txtRobotDiameter";
            this.txtRobotDiameter.Size = new System.Drawing.Size(100, 38);
            this.txtRobotDiameter.TabIndex = 10;
            this.txtRobotDiameter.Text = "0.6";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 205);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 31);
            this.label5.TabIndex = 11;
            this.label5.Text = "Диаметр:";
            // 
            // txtRobotY
            // 
            this.txtRobotY.Location = new System.Drawing.Point(225, 252);
            this.txtRobotY.Name = "txtRobotY";
            this.txtRobotY.Size = new System.Drawing.Size(100, 38);
            this.txtRobotY.TabIndex = 15;
            this.txtRobotY.Text = "1.0";
            // 
            // txtRobotX
            // 
            this.txtRobotX.Location = new System.Drawing.Point(52, 249);
            this.txtRobotX.Name = "txtRobotX";
            this.txtRobotX.Size = new System.Drawing.Size(100, 38);
            this.txtRobotX.TabIndex = 14;
            this.txtRobotX.Text = "1.0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(179, 255);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 31);
            this.label6.TabIndex = 13;
            this.label6.Text = "Y:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 252);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 31);
            this.label7.TabIndex = 12;
            this.label7.Text = "X:";
            // 
            // txtRobotCourse
            // 
            this.txtRobotCourse.Location = new System.Drawing.Point(357, 202);
            this.txtRobotCourse.Name = "txtRobotCourse";
            this.txtRobotCourse.Size = new System.Drawing.Size(100, 38);
            this.txtRobotCourse.TabIndex = 16;
            this.txtRobotCourse.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(268, 205);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 31);
            this.label8.TabIndex = 17;
            this.label8.Text = "Курс:";
            // 
            // btnApplyRobot
            // 
            this.btnApplyRobot.Location = new System.Drawing.Point(12, 293);
            this.btnApplyRobot.Name = "btnApplyRobot";
            this.btnApplyRobot.Size = new System.Drawing.Size(351, 49);
            this.btnApplyRobot.TabIndex = 18;
            this.btnApplyRobot.Text = "Установить параметры";
            this.btnApplyRobot.UseVisualStyleBackColor = true;
            this.btnApplyRobot.Click += new System.EventHandler(this.btnApplyRobot_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Coral;
            this.groupBox2.Controls.Add(this.chkUseConfigSpace);
            this.groupBox2.Controls.Add(this.btnSaveRoute);
            this.groupBox2.Controls.Add(this.chkUsePenalty);
            this.groupBox2.Controls.Add(this.btnFindPath);
            this.groupBox2.Controls.Add(this.btnBuildGraph);
            this.groupBox2.Controls.Add(this.cmbVisualizationStep);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.chkShowConfigSpace);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(977, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(672, 377);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Визуализация";
            // 
            // chkShowConfigSpace
            // 
            this.chkShowConfigSpace.AutoSize = true;
            this.chkShowConfigSpace.Location = new System.Drawing.Point(10, 48);
            this.chkShowConfigSpace.Name = "chkShowConfigSpace";
            this.chkShowConfigSpace.Size = new System.Drawing.Size(410, 35);
            this.chkShowConfigSpace.TabIndex = 0;
            this.chkShowConfigSpace.Text = "Пространство конфигураций";
            this.chkShowConfigSpace.UseVisualStyleBackColor = true;
            this.chkShowConfigSpace.CheckedChanged += new System.EventHandler(this.chkShowConfigSpace_CheckedChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(254, 31);
            this.label9.TabIndex = 1;
            this.label9.Text = "Шаг визуализации:";
            // 
            // cmbVisualizationStep
            // 
            this.cmbVisualizationStep.FormattingEnabled = true;
            this.cmbVisualizationStep.Items.AddRange(new object[] {
            "Все связи",
            "Только видимые связи",
            "Найденный путь"});
            this.cmbVisualizationStep.Location = new System.Drawing.Point(271, 90);
            this.cmbVisualizationStep.Name = "cmbVisualizationStep";
            this.cmbVisualizationStep.Size = new System.Drawing.Size(262, 39);
            this.cmbVisualizationStep.TabIndex = 2;
            this.cmbVisualizationStep.SelectedIndexChanged += new System.EventHandler(this.cmbVisualizationStep_SelectedIndexChanged);
            // 
            // btnBuildGraph
            // 
            this.btnBuildGraph.Location = new System.Drawing.Point(10, 136);
            this.btnBuildGraph.Name = "btnBuildGraph";
            this.btnBuildGraph.Size = new System.Drawing.Size(246, 45);
            this.btnBuildGraph.TabIndex = 3;
            this.btnBuildGraph.Text = "Построить граф";
            this.btnBuildGraph.UseVisualStyleBackColor = true;
            this.btnBuildGraph.Click += new System.EventHandler(this.btnBuildGraph_Click);
            // 
            // btnFindPath
            // 
            this.btnFindPath.Location = new System.Drawing.Point(271, 136);
            this.btnFindPath.Name = "btnFindPath";
            this.btnFindPath.Size = new System.Drawing.Size(262, 45);
            this.btnFindPath.TabIndex = 4;
            this.btnFindPath.Text = "Найти путь";
            this.btnFindPath.UseVisualStyleBackColor = true;
            this.btnFindPath.Click += new System.EventHandler(this.btnFindPath_Click);
            // 
            // chkUsePenalty
            // 
            this.chkUsePenalty.AutoSize = true;
            this.chkUsePenalty.Location = new System.Drawing.Point(10, 198);
            this.chkUsePenalty.Name = "chkUsePenalty";
            this.chkUsePenalty.Size = new System.Drawing.Size(447, 35);
            this.chkUsePenalty.TabIndex = 5;
            this.chkUsePenalty.Text = "Штраф за близкое препядствие";
            this.chkUsePenalty.UseVisualStyleBackColor = true;
            // 
            // btnSaveRoute
            // 
            this.btnSaveRoute.Location = new System.Drawing.Point(10, 240);
            this.btnSaveRoute.Name = "btnSaveRoute";
            this.btnSaveRoute.Size = new System.Drawing.Size(433, 51);
            this.btnSaveRoute.TabIndex = 6;
            this.btnSaveRoute.Text = "Сохранить маршрут в CSV";
            this.btnSaveRoute.UseVisualStyleBackColor = true;
            this.btnSaveRoute.Click += new System.EventHandler(this.btnSaveRoute_Click);
            // 
            // saveFileDialogCSV
            // 
            this.saveFileDialogCSV.Filter = "CSV файлы|*.csv";
            // 
            // chkUseConfigSpace
            // 
            this.chkUseConfigSpace.AutoSize = true;
            this.chkUseConfigSpace.Location = new System.Drawing.Point(10, 311);
            this.chkUseConfigSpace.Name = "chkUseConfigSpace";
            this.chkUseConfigSpace.Size = new System.Drawing.Size(589, 35);
            this.chkUseConfigSpace.TabIndex = 7;
            this.chkUseConfigSpace.Text = "Использовать пространство конфигурации";
            this.chkUseConfigSpace.UseVisualStyleBackColor = true;
            this.chkUseConfigSpace.CheckedChanged += new System.EventHandler(this.chkUseConfigSpace_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SandyBrown;
            this.ClientSize = new System.Drawing.Size(2052, 1054);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnLoadMap);
            this.Controls.Add(this.listBoxRawData);
            this.Controls.Add(this.pictureBoxMap);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Murashkin Ivan 241-324 Lab 5";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMap)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialogMap;
        private System.Windows.Forms.PictureBox pictureBoxMap;
        private System.Windows.Forms.ListBox listBoxRawData;
        private System.Windows.Forms.Button btnLoadMap;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnApplyEnd;
        private System.Windows.Forms.TextBox txtEndY;
        private System.Windows.Forms.TextBox txtEndX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnApplyStart;
        private System.Windows.Forms.TextBox txtStartY;
        private System.Windows.Forms.TextBox txtStartX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRobotY;
        private System.Windows.Forms.TextBox txtRobotX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtRobotDiameter;
        private System.Windows.Forms.Button btnApplyRobot;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtRobotCourse;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chkShowConfigSpace;
        private System.Windows.Forms.ComboBox cmbVisualizationStep;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkUsePenalty;
        private System.Windows.Forms.Button btnFindPath;
        private System.Windows.Forms.Button btnBuildGraph;
        private System.Windows.Forms.Button btnSaveRoute;
        private System.Windows.Forms.SaveFileDialog saveFileDialogCSV;
        private System.Windows.Forms.CheckBox chkUseConfigSpace;
    }
}

