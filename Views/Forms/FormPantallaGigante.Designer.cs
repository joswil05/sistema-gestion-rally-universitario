namespace SistemadeGestiondeRallyUniversitario.Views.Forms
{
    partial class FormPantallaGigante
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.lblEnVivo = new System.Windows.Forms.Label();
            this.lblHora = new System.Windows.Forms.Label();
            this.lblSubtitulo = new System.Windows.Forms.Label();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelPodioCards = new System.Windows.Forms.TableLayoutPanel();
            this.cardPlata = new System.Windows.Forms.Panel();
            this.lblPlataRetos = new System.Windows.Forms.Label();
            this.lblPlataTiempo = new System.Windows.Forms.Label();
            this.lblPlataCarrera = new System.Windows.Forms.Label();
            this.lblPlataTitulo = new System.Windows.Forms.Label();
            this.cardOro = new System.Windows.Forms.Panel();
            this.lblOroRetos = new System.Windows.Forms.Label();
            this.lblOroTiempo = new System.Windows.Forms.Label();
            this.lblOroCarrera = new System.Windows.Forms.Label();
            this.lblOroTitulo = new System.Windows.Forms.Label();
            this.cardBronce = new System.Windows.Forms.Panel();
            this.lblBronceRetos = new System.Windows.Forms.Label();
            this.lblBronceTiempo = new System.Windows.Forms.Label();
            this.lblBronceCarrera = new System.Windows.Forms.Label();
            this.lblBronceTitulo = new System.Windows.Forms.Label();
            this.panelTabla = new System.Windows.Forms.Panel();
            this.dgvLeaderboard = new System.Windows.Forms.DataGridView();
            this.lblTituloTabla = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelPodioCards.SuspendLayout();
            this.cardPlata.SuspendLayout();
            this.cardOro.SuspendLayout();
            this.cardBronce.SuspendLayout();
            this.panelTabla.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeaderboard)).BeginInit();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.panelHeader.Controls.Add(this.btnCerrar);
            this.panelHeader.Controls.Add(this.lblEnVivo);
            this.panelHeader.Controls.Add(this.lblHora);
            this.panelHeader.Controls.Add(this.lblSubtitulo);
            this.panelHeader.Controls.Add(this.lblTitulo);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Padding = new System.Windows.Forms.Padding(25, 15, 25, 15);
            this.panelHeader.Size = new System.Drawing.Size(1200, 85);
            this.panelHeader.TabIndex = 0;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(1055, 22);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(120, 38);
            this.btnCerrar.TabIndex = 4;
            this.btnCerrar.Text = "✕ Salir";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // lblEnVivo
            // 
            this.lblEnVivo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblEnVivo.AutoSize = true;
            this.lblEnVivo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.lblEnVivo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnVivo.ForeColor = System.Drawing.Color.White;
            this.lblEnVivo.Location = new System.Drawing.Point(920, 34);
            this.lblEnVivo.Name = "lblEnVivo";
            this.lblEnVivo.Padding = new System.Windows.Forms.Padding(6, 3, 6, 3);
            this.lblEnVivo.Size = new System.Drawing.Size(89, 21);
            this.lblEnVivo.TabIndex = 3;
            this.lblEnVivo.Text = "● EN VIVO";
            // 
            // lblHora
            // 
            this.lblHora.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblHora.AutoSize = true;
            this.lblHora.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHora.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblHora.Location = new System.Drawing.Point(810, 30);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(88, 25);
            this.lblHora.TabIndex = 2;
            this.lblHora.Text = "00:00:00";
            // 
            // lblSubtitulo
            // 
            this.lblSubtitulo.AutoSize = true;
            this.lblSubtitulo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubtitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSubtitulo.Location = new System.Drawing.Point(24, 46);
            this.lblSubtitulo.Name = "lblSubtitulo";
            this.lblSubtitulo.Size = new System.Drawing.Size(374, 20);
            this.lblSubtitulo.TabIndex = 1;
            this.lblSubtitulo.Text = "TABLERO OFICIAL DE POSICIONES Y PODIO DEL RALLY";
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(222)))), ((int)(((byte)(128)))));
            this.lblTitulo.Location = new System.Drawing.Point(21, 10);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(437, 37);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "🏆 RALLY UNIVERSITARIO ULSA";
            // 
            // panelPodioCards
            // 
            this.panelPodioCards.ColumnCount = 3;
            this.panelPodioCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.panelPodioCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.panelPodioCards.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.panelPodioCards.Controls.Add(this.cardPlata, 0, 0);
            this.panelPodioCards.Controls.Add(this.cardOro, 1, 0);
            this.panelPodioCards.Controls.Add(this.cardBronce, 2, 0);
            this.panelPodioCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPodioCards.Location = new System.Drawing.Point(0, 85);
            this.panelPodioCards.Name = "panelPodioCards";
            this.panelPodioCards.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.panelPodioCards.RowCount = 1;
            this.panelPodioCards.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.panelPodioCards.Size = new System.Drawing.Size(1200, 210);
            this.panelPodioCards.TabIndex = 1;
            // 
            // cardPlata
            // 
            this.cardPlata.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.cardPlata.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardPlata.Controls.Add(this.lblPlataRetos);
            this.cardPlata.Controls.Add(this.lblPlataTiempo);
            this.cardPlata.Controls.Add(this.lblPlataCarrera);
            this.cardPlata.Controls.Add(this.lblPlataTitulo);
            this.cardPlata.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardPlata.Location = new System.Drawing.Point(23, 18);
            this.cardPlata.Name = "cardPlata";
            this.cardPlata.Padding = new System.Windows.Forms.Padding(12);
            this.cardPlata.Size = new System.Drawing.Size(380, 174);
            this.cardPlata.TabIndex = 0;
            // 
            // lblPlataRetos
            // 
            this.lblPlataRetos.AutoSize = true;
            this.lblPlataRetos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlataRetos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblPlataRetos.Location = new System.Drawing.Point(12, 135);
            this.lblPlataRetos.Name = "lblPlataRetos";
            this.lblPlataRetos.Size = new System.Drawing.Size(140, 19);
            this.lblPlataRetos.TabIndex = 3;
            this.lblPlataRetos.Text = "Retos Completados: 0";
            // 
            // lblPlataTiempo
            // 
            this.lblPlataTiempo.AutoSize = true;
            this.lblPlataTiempo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlataTiempo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblPlataTiempo.Location = new System.Drawing.Point(10, 92);
            this.lblPlataTiempo.Name = "lblPlataTiempo";
            this.lblPlataTiempo.Size = new System.Drawing.Size(111, 32);
            this.lblPlataTiempo.TabIndex = 2;
            this.lblPlataTiempo.Text = "00:00.00";
            // 
            // lblPlataCarrera
            // 
            this.lblPlataCarrera.AutoEllipsis = true;
            this.lblPlataCarrera.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlataCarrera.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblPlataCarrera.Location = new System.Drawing.Point(11, 45);
            this.lblPlataCarrera.Name = "lblPlataCarrera";
            this.lblPlataCarrera.Size = new System.Drawing.Size(350, 42);
            this.lblPlataCarrera.TabIndex = 1;
            this.lblPlataCarrera.Text = "En espera de resultados...";
            // 
            // lblPlataTitulo
            // 
            this.lblPlataTitulo.AutoSize = true;
            this.lblPlataTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlataTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblPlataTitulo.Location = new System.Drawing.Point(10, 12);
            this.lblPlataTitulo.Name = "lblPlataTitulo";
            this.lblPlataTitulo.Size = new System.Drawing.Size(183, 21);
            this.lblPlataTitulo.TabIndex = 0;
            this.lblPlataTitulo.Text = "🥈 2° LUGAR - PLATA";
            // 
            // cardOro
            // 
            this.cardOro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(240)))), ((int)(((byte)(138)))));
            this.cardOro.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardOro.Controls.Add(this.lblOroRetos);
            this.cardOro.Controls.Add(this.lblOroTiempo);
            this.cardOro.Controls.Add(this.lblOroCarrera);
            this.cardOro.Controls.Add(this.lblOroTitulo);
            this.cardOro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardOro.Location = new System.Drawing.Point(409, 18);
            this.cardOro.Name = "cardOro";
            this.cardOro.Padding = new System.Windows.Forms.Padding(12);
            this.cardOro.Size = new System.Drawing.Size(380, 174);
            this.cardOro.TabIndex = 1;
            // 
            // lblOroRetos
            // 
            this.lblOroRetos.AutoSize = true;
            this.lblOroRetos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOroRetos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(63)))), ((int)(((byte)(18)))));
            this.lblOroRetos.Location = new System.Drawing.Point(12, 135);
            this.lblOroRetos.Name = "lblOroRetos";
            this.lblOroRetos.Size = new System.Drawing.Size(157, 19);
            this.lblOroRetos.TabIndex = 3;
            this.lblOroRetos.Text = "Retos Completados: 0";
            // 
            // lblOroTiempo
            // 
            this.lblOroTiempo.AutoSize = true;
            this.lblOroTiempo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOroTiempo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(63)))), ((int)(((byte)(18)))));
            this.lblOroTiempo.Location = new System.Drawing.Point(10, 88);
            this.lblOroTiempo.Name = "lblOroTiempo";
            this.lblOroTiempo.Size = new System.Drawing.Size(126, 37);
            this.lblOroTiempo.TabIndex = 2;
            this.lblOroTiempo.Text = "00:00.00";
            // 
            // lblOroCarrera
            // 
            this.lblOroCarrera.AutoEllipsis = true;
            this.lblOroCarrera.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOroCarrera.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(113)))), ((int)(((byte)(63)))), ((int)(((byte)(18)))));
            this.lblOroCarrera.Location = new System.Drawing.Point(11, 42);
            this.lblOroCarrera.Name = "lblOroCarrera";
            this.lblOroCarrera.Size = new System.Drawing.Size(350, 42);
            this.lblOroCarrera.TabIndex = 1;
            this.lblOroCarrera.Text = "En espera de resultados...";
            // 
            // lblOroTitulo
            // 
            this.lblOroTitulo.AutoSize = true;
            this.lblOroTitulo.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOroTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(161)))), ((int)(((byte)(98)))), ((int)(((byte)(7)))));
            this.lblOroTitulo.Location = new System.Drawing.Point(10, 10);
            this.lblOroTitulo.Name = "lblOroTitulo";
            this.lblOroTitulo.Size = new System.Drawing.Size(288, 25);
            this.lblOroTitulo.TabIndex = 0;
            this.lblOroTitulo.Text = "🥇 1° LUGAR - LÍDER DE RALLY";
            // 
            // cardBronce
            // 
            this.cardBronce.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(213)))));
            this.cardBronce.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cardBronce.Controls.Add(this.lblBronceRetos);
            this.cardBronce.Controls.Add(this.lblBronceTiempo);
            this.cardBronce.Controls.Add(this.lblBronceCarrera);
            this.cardBronce.Controls.Add(this.lblBronceTitulo);
            this.cardBronce.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardBronce.Location = new System.Drawing.Point(795, 18);
            this.cardBronce.Name = "cardBronce";
            this.cardBronce.Padding = new System.Windows.Forms.Padding(12);
            this.cardBronce.Size = new System.Drawing.Size(382, 174);
            this.cardBronce.TabIndex = 2;
            // 
            // lblBronceRetos
            // 
            this.lblBronceRetos.AutoSize = true;
            this.lblBronceRetos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBronceRetos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(45)))), ((int)(((byte)(18)))));
            this.lblBronceRetos.Location = new System.Drawing.Point(12, 135);
            this.lblBronceRetos.Name = "lblBronceRetos";
            this.lblBronceRetos.Size = new System.Drawing.Size(140, 19);
            this.lblBronceRetos.TabIndex = 3;
            this.lblBronceRetos.Text = "Retos Completados: 0";
            // 
            // lblBronceTiempo
            // 
            this.lblBronceTiempo.AutoSize = true;
            this.lblBronceTiempo.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBronceTiempo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(52)))), ((int)(((byte)(18)))));
            this.lblBronceTiempo.Location = new System.Drawing.Point(10, 92);
            this.lblBronceTiempo.Name = "lblBronceTiempo";
            this.lblBronceTiempo.Size = new System.Drawing.Size(111, 32);
            this.lblBronceTiempo.TabIndex = 2;
            this.lblBronceTiempo.Text = "00:00.00";
            // 
            // lblBronceCarrera
            // 
            this.lblBronceCarrera.AutoEllipsis = true;
            this.lblBronceCarrera.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBronceCarrera.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(45)))), ((int)(((byte)(18)))));
            this.lblBronceCarrera.Location = new System.Drawing.Point(11, 45);
            this.lblBronceCarrera.Name = "lblBronceCarrera";
            this.lblBronceCarrera.Size = new System.Drawing.Size(350, 42);
            this.lblBronceCarrera.TabIndex = 1;
            this.lblBronceCarrera.Text = "En espera de resultados...";
            // 
            // lblBronceTitulo
            // 
            this.lblBronceTitulo.AutoSize = true;
            this.lblBronceTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBronceTitulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(65)))), ((int)(((byte)(12)))));
            this.lblBronceTitulo.Location = new System.Drawing.Point(10, 12);
            this.lblBronceTitulo.Name = "lblBronceTitulo";
            this.lblBronceTitulo.Size = new System.Drawing.Size(201, 21);
            this.lblBronceTitulo.TabIndex = 0;
            this.lblBronceTitulo.Text = "🥉 3° LUGAR - BRONCE";
            // 
            // panelTabla
            // 
            this.panelTabla.BackColor = System.Drawing.Color.White;
            this.panelTabla.Controls.Add(this.dgvLeaderboard);
            this.panelTabla.Controls.Add(this.lblTituloTabla);
            this.panelTabla.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTabla.Location = new System.Drawing.Point(0, 295);
            this.panelTabla.Name = "panelTabla";
            this.panelTabla.Padding = new System.Windows.Forms.Padding(20);
            this.panelTabla.Size = new System.Drawing.Size(1200, 405);
            this.panelTabla.TabIndex = 2;
            // 
            // dgvLeaderboard
            // 
            this.dgvLeaderboard.AllowUserToAddRows = false;
            this.dgvLeaderboard.AllowUserToDeleteRows = false;
            this.dgvLeaderboard.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLeaderboard.BackgroundColor = System.Drawing.Color.White;
            this.dgvLeaderboard.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLeaderboard.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvLeaderboard.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Padding = new System.Windows.Forms.Padding(6);
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvLeaderboard.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvLeaderboard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(6);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvLeaderboard.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvLeaderboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLeaderboard.EnableHeadersVisualStyles = false;
            this.dgvLeaderboard.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.dgvLeaderboard.Location = new System.Drawing.Point(20, 50);
            this.dgvLeaderboard.MultiSelect = false;
            this.dgvLeaderboard.Name = "dgvLeaderboard";
            this.dgvLeaderboard.ReadOnly = true;
            this.dgvLeaderboard.RowHeadersVisible = false;
            this.dgvLeaderboard.RowTemplate.Height = 40;
            this.dgvLeaderboard.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLeaderboard.Size = new System.Drawing.Size(1160, 335);
            this.dgvLeaderboard.TabIndex = 1;
            // 
            // lblTituloTabla
            // 
            this.lblTituloTabla.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTituloTabla.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloTabla.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTituloTabla.Location = new System.Drawing.Point(20, 20);
            this.lblTituloTabla.Name = "lblTituloTabla";
            this.lblTituloTabla.Size = new System.Drawing.Size(1160, 30);
            this.lblTituloTabla.TabIndex = 0;
            this.lblTituloTabla.Text = "Clasificación General Completa por Carrera";
            // 
            // FormPantallaGigante
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.panelTabla);
            this.Controls.Add(this.panelPodioCards);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FormPantallaGigante";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Live Scoreboard - Rally Universitario ULSA";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormPantallaGigante_Load);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelPodioCards.ResumeLayout(false);
            this.cardPlata.ResumeLayout(false);
            this.cardPlata.PerformLayout();
            this.cardOro.ResumeLayout(false);
            this.cardOro.PerformLayout();
            this.cardBronce.ResumeLayout(false);
            this.cardBronce.PerformLayout();
            this.panelTabla.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeaderboard)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSubtitulo;
        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.Label lblEnVivo;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.TableLayoutPanel panelPodioCards;
        private System.Windows.Forms.Panel cardPlata;
        private System.Windows.Forms.Label lblPlataTitulo;
        private System.Windows.Forms.Label lblPlataCarrera;
        private System.Windows.Forms.Label lblPlataTiempo;
        private System.Windows.Forms.Label lblPlataRetos;
        private System.Windows.Forms.Panel cardOro;
        private System.Windows.Forms.Label lblOroTitulo;
        private System.Windows.Forms.Label lblOroCarrera;
        private System.Windows.Forms.Label lblOroTiempo;
        private System.Windows.Forms.Label lblOroRetos;
        private System.Windows.Forms.Panel cardBronce;
        private System.Windows.Forms.Label lblBronceTitulo;
        private System.Windows.Forms.Label lblBronceCarrera;
        private System.Windows.Forms.Label lblBronceTiempo;
        private System.Windows.Forms.Label lblBronceRetos;
        private System.Windows.Forms.Panel panelTabla;
        private System.Windows.Forms.Label lblTituloTabla;
        private System.Windows.Forms.DataGridView dgvLeaderboard;
    }
}
