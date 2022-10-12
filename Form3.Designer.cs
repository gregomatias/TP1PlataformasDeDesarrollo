namespace TP1
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_crearCajaAhorro = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button3 = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dataGridView4 = new System.Windows.Forms.DataGridView();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtb_monto = new System.Windows.Forms.TextBox();
            this.btn_depositar = new System.Windows.Forms.Button();
            this.btn_extraer = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.txtb_cbu_destino = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtb_monto_transferencia = new System.Windows.Forms.TextBox();
            this.btn_transferir = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.dateTimePicker_filtro = new System.Windows.Forms.DateTimePicker();
            this.txtb_filtro_detalle = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtb_filtro_monto = new System.Windows.Forms.TextBox();
            this.btn_busca_movimiento = new System.Windows.Forms.Button();
            this.comboBox3_movimientos = new System.Windows.Forms.ComboBox();
            this.dataGridView_movimiento = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.tabPage5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_movimiento)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dataGridView1.Location = new System.Drawing.Point(6, 5);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 29;
            this.dataGridView1.Size = new System.Drawing.Size(441, 232);
            this.dataGridView1.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "CBU";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 125;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Saldo";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 125;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Roboto", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(238)))), ((int)(((byte)(236)))));
            this.label1.Location = new System.Drawing.Point(636, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Bienvenido";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(238)))), ((int)(((byte)(236)))));
            this.label2.Location = new System.Drawing.Point(735, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(134, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "lbl_nombreUsuario";
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::TP1.Properties.Resources.refresh_wght39;
            this.button1.Enabled = false;
            this.button1.Location = new System.Drawing.Point(638, 131);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 36);
            this.button1.TabIndex = 3;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Location = new System.Drawing.Point(26, 104);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(667, 290);
            this.tabControl1.TabIndex = 4;
            this.tabControl1.TabStop = false;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_crearCajaAhorro);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new System.Drawing.Point(4, 27);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(659, 259);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Caja de Ahorro";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // btn_crearCajaAhorro
            // 
            this.btn_crearCajaAhorro.Location = new System.Drawing.Point(478, 24);
            this.btn_crearCajaAhorro.Name = "btn_crearCajaAhorro";
            this.btn_crearCajaAhorro.Size = new System.Drawing.Size(114, 46);
            this.btn_crearCajaAhorro.TabIndex = 2;
            this.btn_crearCajaAhorro.Text = " Crear Caja";
            this.btn_crearCajaAhorro.UseVisualStyleBackColor = true;
            this.btn_crearCajaAhorro.Click += new System.EventHandler(this.btn_crearCajaAhorro_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.dataGridView2);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(659, 257);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Plazo Fijo";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(334, 20);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(94, 26);
            this.button3.TabIndex = 1;
            this.button3.Text = "Crear";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(6, 5);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 29;
            this.dataGridView2.Size = new System.Drawing.Size(300, 169);
            this.dataGridView2.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dataGridView4);
            this.tabPage3.Controls.Add(this.dataGridView3);
            this.tabPage3.Location = new System.Drawing.Point(4, 29);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(659, 257);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Pagos";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataGridView4
            // 
            this.dataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView4.Location = new System.Drawing.Point(325, 31);
            this.dataGridView4.Name = "dataGridView4";
            this.dataGridView4.RowHeadersWidth = 51;
            this.dataGridView4.RowTemplate.Height = 29;
            this.dataGridView4.Size = new System.Drawing.Size(250, 169);
            this.dataGridView4.TabIndex = 1;
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(15, 31);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersWidth = 51;
            this.dataGridView3.RowTemplate.Height = 29;
            this.dataGridView3.Size = new System.Drawing.Size(251, 169);
            this.dataGridView3.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 29);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(659, 257);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Tarjetas";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.label5);
            this.tabPage5.Controls.Add(this.label4);
            this.tabPage5.Controls.Add(this.txtb_monto);
            this.tabPage5.Controls.Add(this.btn_depositar);
            this.tabPage5.Controls.Add(this.btn_extraer);
            this.tabPage5.Controls.Add(this.comboBox1);
            this.tabPage5.Location = new System.Drawing.Point(4, 29);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(659, 257);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Transacciones";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(374, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 18);
            this.label5.TabIndex = 7;
            this.label5.Text = "Ingrese el monto:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 18);
            this.label4.TabIndex = 6;
            this.label4.Text = "Seleccione la cuenta:";
            // 
            // txtb_monto
            // 
            this.txtb_monto.Enabled = false;
            this.txtb_monto.Location = new System.Drawing.Point(430, 43);
            this.txtb_monto.Name = "txtb_monto";
            this.txtb_monto.PlaceholderText = "Ingrese el monto";
            this.txtb_monto.Size = new System.Drawing.Size(125, 26);
            this.txtb_monto.TabIndex = 5;
            this.txtb_monto.TextChanged += new System.EventHandler(this.txtb_monto_TextChanged);
            // 
            // btn_depositar
            // 
            this.btn_depositar.Enabled = false;
            this.btn_depositar.Location = new System.Drawing.Point(386, 145);
            this.btn_depositar.Name = "btn_depositar";
            this.btn_depositar.Size = new System.Drawing.Size(94, 29);
            this.btn_depositar.TabIndex = 4;
            this.btn_depositar.Text = "Depositar";
            this.btn_depositar.UseVisualStyleBackColor = true;
            this.btn_depositar.Click += new System.EventHandler(this.btn_depositar_Click);
            // 
            // btn_extraer
            // 
            this.btn_extraer.Enabled = false;
            this.btn_extraer.Location = new System.Drawing.Point(152, 145);
            this.btn_extraer.Name = "btn_extraer";
            this.btn_extraer.Size = new System.Drawing.Size(94, 29);
            this.btn_extraer.TabIndex = 3;
            this.btn_extraer.Text = "Extraer";
            this.btn_extraer.UseVisualStyleBackColor = true;
            this.btn_extraer.Click += new System.EventHandler(this.btn_extraer_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(64, 43);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(151, 26);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
            this.comboBox1.Click += new System.EventHandler(this.comboBox1_Click);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.txtb_cbu_destino);
            this.tabPage6.Controls.Add(this.label8);
            this.tabPage6.Controls.Add(this.label6);
            this.tabPage6.Controls.Add(this.label7);
            this.tabPage6.Controls.Add(this.txtb_monto_transferencia);
            this.tabPage6.Controls.Add(this.btn_transferir);
            this.tabPage6.Controls.Add(this.comboBox2);
            this.tabPage6.Location = new System.Drawing.Point(4, 27);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(659, 259);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Transferencia";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // txtb_cbu_destino
            // 
            this.txtb_cbu_destino.Location = new System.Drawing.Point(121, 137);
            this.txtb_cbu_destino.Name = "txtb_cbu_destino";
            this.txtb_cbu_destino.Size = new System.Drawing.Size(125, 26);
            this.txtb_cbu_destino.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(21, 107);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(159, 18);
            this.label8.TabIndex = 14;
            this.label8.Text = "Ingrese el cbu destino:";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(327, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(202, 18);
            this.label6.TabIndex = 12;
            this.label6.Text = "Ingrese el monto a transferir:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(155, 18);
            this.label7.TabIndex = 11;
            this.label7.Text = "Seleccione su cuenta:";
            // 
            // txtb_monto_transferencia
            // 
            this.txtb_monto_transferencia.Enabled = false;
            this.txtb_monto_transferencia.Location = new System.Drawing.Point(517, 96);
            this.txtb_monto_transferencia.Name = "txtb_monto_transferencia";
            this.txtb_monto_transferencia.PlaceholderText = "Ingrese el monto";
            this.txtb_monto_transferencia.Size = new System.Drawing.Size(125, 26);
            this.txtb_monto_transferencia.TabIndex = 10;
            // 
            // btn_transferir
            // 
            this.btn_transferir.Enabled = false;
            this.btn_transferir.Location = new System.Drawing.Point(282, 180);
            this.btn_transferir.Name = "btn_transferir";
            this.btn_transferir.Size = new System.Drawing.Size(94, 29);
            this.btn_transferir.TabIndex = 9;
            this.btn_transferir.Text = "Transferir";
            this.btn_transferir.UseVisualStyleBackColor = true;
            this.btn_transferir.Click += new System.EventHandler(this.btn_transferir_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(121, 48);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(151, 26);
            this.comboBox2.TabIndex = 8;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            this.comboBox2.Click += new System.EventHandler(this.comboBox2_Click);
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.dateTimePicker_filtro);
            this.tabPage7.Controls.Add(this.txtb_filtro_detalle);
            this.tabPage7.Controls.Add(this.label9);
            this.tabPage7.Controls.Add(this.label10);
            this.tabPage7.Controls.Add(this.txtb_filtro_monto);
            this.tabPage7.Controls.Add(this.btn_busca_movimiento);
            this.tabPage7.Controls.Add(this.comboBox3_movimientos);
            this.tabPage7.Controls.Add(this.dataGridView_movimiento);
            this.tabPage7.Location = new System.Drawing.Point(4, 29);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage7.Size = new System.Drawing.Size(659, 257);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "Movimientos";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker_filtro
            // 
            this.dateTimePicker_filtro.Location = new System.Drawing.Point(470, 184);
            this.dateTimePicker_filtro.Name = "dateTimePicker_filtro";
            this.dateTimePicker_filtro.Size = new System.Drawing.Size(183, 26);
            this.dateTimePicker_filtro.TabIndex = 6;
            // 
            // txtb_filtro_detalle
            // 
            this.txtb_filtro_detalle.Location = new System.Drawing.Point(506, 143);
            this.txtb_filtro_detalle.Name = "txtb_filtro_detalle";
            this.txtb_filtro_detalle.PlaceholderText = "Detalle";
            this.txtb_filtro_detalle.Size = new System.Drawing.Size(125, 26);
            this.txtb_filtro_detalle.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(471, 83);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 18);
            this.label9.TabIndex = 13;
            this.label9.Text = "Filtros:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(471, 17);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(151, 18);
            this.label10.TabIndex = 12;
            this.label10.Text = "Seleccione la cuenta:";
            // 
            // txtb_filtro_monto
            // 
            this.txtb_filtro_monto.Location = new System.Drawing.Point(506, 111);
            this.txtb_filtro_monto.Name = "txtb_filtro_monto";
            this.txtb_filtro_monto.PlaceholderText = "Monto";
            this.txtb_filtro_monto.Size = new System.Drawing.Size(125, 26);
            this.txtb_filtro_monto.TabIndex = 11;
            // 
            // btn_busca_movimiento
            // 
            this.btn_busca_movimiento.Location = new System.Drawing.Point(528, 216);
            this.btn_busca_movimiento.Name = "btn_busca_movimiento";
            this.btn_busca_movimiento.Size = new System.Drawing.Size(94, 29);
            this.btn_busca_movimiento.TabIndex = 10;
            this.btn_busca_movimiento.Text = "Buscar";
            this.btn_busca_movimiento.UseVisualStyleBackColor = true;
            this.btn_busca_movimiento.Click += new System.EventHandler(this.btn_busca_movimiento_Click);
            // 
            // comboBox3_movimientos
            // 
            this.comboBox3_movimientos.FormattingEnabled = true;
            this.comboBox3_movimientos.Location = new System.Drawing.Point(496, 42);
            this.comboBox3_movimientos.Name = "comboBox3_movimientos";
            this.comboBox3_movimientos.Size = new System.Drawing.Size(151, 26);
            this.comboBox3_movimientos.TabIndex = 9;
            this.comboBox3_movimientos.Click += new System.EventHandler(this.comboBox3_movimientos_Click);
            // 
            // dataGridView_movimiento
            // 
            this.dataGridView_movimiento.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_movimiento.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.dataGridView_movimiento.Location = new System.Drawing.Point(11, 13);
            this.dataGridView_movimiento.Name = "dataGridView_movimiento";
            this.dataGridView_movimiento.RowHeadersWidth = 51;
            this.dataGridView_movimiento.RowTemplate.Height = 29;
            this.dataGridView_movimiento.Size = new System.Drawing.Size(441, 232);
            this.dataGridView_movimiento.TabIndex = 8;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Caja";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 50;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "Detalle";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 160;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "Monto";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 85;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Fecha";
            this.dataGridViewTextBoxColumn8.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Width = 90;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(41)))), ((int)(((byte)(30)))));
            this.panel3.Controls.Add(this.button4);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(935, 52);
            this.panel3.TabIndex = 5;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(41)))), ((int)(((byte)(30)))));
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Roboto", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.button4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.button4.Location = new System.Drawing.Point(897, 9);
            this.button4.Margin = new System.Windows.Forms.Padding(0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(29, 29);
            this.button4.TabIndex = 4;
            this.button4.Text = "X";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.ForeColor = System.Drawing.SystemColors.Window;
            this.label3.Location = new System.Drawing.Point(10, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 29);
            this.label3.TabIndex = 3;
            this.label3.Text = "Online Banking";
            // 
            // Form3
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(238)))), ((int)(((byte)(236)))));
            this.ClientSize = new System.Drawing.Size(935, 451);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form3";
            this.Load += new System.EventHandler(this.Form3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tabPage6.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.tabPage7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_movimiento)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Button button3;
        private DataGridView dataGridView2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridView dataGridView4;
        private DataGridView dataGridView3;
        private Panel panel3;
        private Label label3;
        private ColorDialog colorDialog1;
        private Button btn_crearCajaAhorro;
        private Button button4;
        private TabPage tabPage5;
        private ComboBox comboBox1;
        private Button btn_depositar;
        private Button btn_extraer;
        private TextBox txtb_monto;
        private Label label5;
        private Label label4;
        private TabPage tabPage6;
        private Label label8;
        private Label label6;
        private Label label7;
        private TextBox txtb_monto_transferencia;
        private Button btn_transferir;
        private ComboBox comboBox2;
        private TextBox txtb_cbu_destino;
        private TabPage tabPage7;
        private TextBox txtb_filtro_detalle;
        private Label label9;
        private Label label10;
        private TextBox txtb_filtro_monto;
        private Button btn_busca_movimiento;
        private ComboBox comboBox3_movimientos;
        private DataGridView dataGridView_movimiento;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DateTimePicker dateTimePicker_filtro;
    }

}