namespace SistemadeGestiondeRallyUniversitario
{
    partial class PaginaInicio
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
            this.panelMenuLateral = new System.Windows.Forms.Panel();
            this.btnSalir = new System.Windows.Forms.Button();
            this.btnClasificacion = new System.Windows.Forms.Button();
            this.btnResultados = new System.Windows.Forms.Button();
            this.btnRetos = new System.Windows.Forms.Button();
            this.btnEstudiantes = new System.Windows.Forms.Button();
            this.btnCarreras = new System.Windows.Forms.Button();
            this.btnInicio = new System.Windows.Forms.Button();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.btnMenuLateral = new System.Windows.Forms.Button();
            this.lblTituloApp = new System.Windows.Forms.Label();
            this.panelTopHeader = new System.Windows.Forms.Panel();
            this.lblUsuarioSesion = new System.Windows.Forms.Label();
            this.lblEvento = new System.Windows.Forms.Label();
            this.panelContenidoPrincipal = new System.Windows.Forms.Panel();
            this.panelMenuLateral.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.panelTopHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // 
            // panelMenuLateral
            // 
            this.panelMenuLateral.BackColor = System.Drawing.Color.White;
            this.panelMenuLateral.Controls.Add(this.btnSalir);
            this.panelMenuLateral.Controls.Add(this.btnClasificacion);
            this.panelMenuLateral.Controls.Add(this.btnResultados);
            this.panelMenuLateral.Controls.Add(this.btnRetos);
            this.panelMenuLateral.Controls.Add(this.btnEstudiantes);
            this.panelMenuLateral.Controls.Add(this.btnCarreras);
            this.panelMenuLateral.Controls.Add(this.btnInicio);
            this.panelMenuLateral.Controls.Add(this.panelLogo);
            this.panelMenuLateral.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenuLateral.Location = new System.Drawing.Point(0, 0);
            this.panelMenuLateral.Name = "panelMenuLateral";
            this.panelMenuLateral.Size = new System.Drawing.Size(210, 681);
            this.panelMenuLateral.TabIndex = 0;
            this.panelMenuLateral.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMenuLateral_Paint);
            // 
            // btnSalir
            // 
            this.btnSalir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalir.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSalir.FlatAppearance.BorderSize = 0;
            this.btnSalir.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(202)))), ((int)(((byte)(202)))));
            this.btnSalir.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalir.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnSalir.Location = new System.Drawing.Point(0, 631);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnSalir.Size = new System.Drawing.Size(210, 50);
            this.btnSalir.TabIndex = 7;
            this.btnSalir.Text = "🚪  Cerrar Sesión";
            this.btnSalir.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // btnClasificacion
            // 
            this.btnClasificacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClasificacion.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnClasificacion.FlatAppearance.BorderSize = 0;
            this.btnClasificacion.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            this.btnClasificacion.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.btnClasificacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClasificacion.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClasificacion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnClasificacion.Location = new System.Drawing.Point(0, 310);
            this.btnClasificacion.Name = "btnClasificacion";
            this.btnClasificacion.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnClasificacion.Size = new System.Drawing.Size(210, 50);
            this.btnClasificacion.TabIndex = 6;
            this.btnClasificacion.Text = "🏆  Clasificación";
            this.btnClasificacion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClasificacion.UseVisualStyleBackColor = true;
            this.btnClasificacion.Click += new System.EventHandler(this.btnClasificacion_Click);
            // 
            // btnResultados
            // 
            this.btnResultados.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnResultados.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnResultados.FlatAppearance.BorderSize = 0;
            this.btnResultados.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            this.btnResultados.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.btnResultados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResultados.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResultados.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnResultados.Location = new System.Drawing.Point(0, 260);
            this.btnResultados.Name = "btnResultados";
            this.btnResultados.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnResultados.Size = new System.Drawing.Size(210, 50);
            this.btnResultados.TabIndex = 5;
            this.btnResultados.Text = "⏱  Resultados";
            this.btnResultados.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnResultados.UseVisualStyleBackColor = true;
            this.btnResultados.Click += new System.EventHandler(this.btnResultados_Click);
            // 
            // btnRetos
            // 
            this.btnRetos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRetos.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRetos.FlatAppearance.BorderSize = 0;
            this.btnRetos.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            this.btnRetos.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.btnRetos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRetos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRetos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnRetos.Location = new System.Drawing.Point(0, 210);
            this.btnRetos.Name = "btnRetos";
            this.btnRetos.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnRetos.Size = new System.Drawing.Size(210, 50);
            this.btnRetos.TabIndex = 4;
            this.btnRetos.Text = "🎯  Retos";
            this.btnRetos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRetos.UseVisualStyleBackColor = true;
            this.btnRetos.Click += new System.EventHandler(this.btnRetos_Click);
            // 
            // btnEstudiantes
            // 
            this.btnEstudiantes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEstudiantes.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnEstudiantes.FlatAppearance.BorderSize = 0;
            this.btnEstudiantes.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            this.btnEstudiantes.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.btnEstudiantes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEstudiantes.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEstudiantes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnEstudiantes.Location = new System.Drawing.Point(0, 160);
            this.btnEstudiantes.Name = "btnEstudiantes";
            this.btnEstudiantes.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnEstudiantes.Size = new System.Drawing.Size(210, 50);
            this.btnEstudiantes.TabIndex = 3;
            this.btnEstudiantes.Text = "👥  Estudiantes";
            this.btnEstudiantes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEstudiantes.UseVisualStyleBackColor = true;
            this.btnEstudiantes.Click += new System.EventHandler(this.btnEstudiantes_Click);
            // 
            // btnCarreras
            // 
            this.btnCarreras.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCarreras.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCarreras.FlatAppearance.BorderSize = 0;
            this.btnCarreras.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            this.btnCarreras.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.btnCarreras.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCarreras.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCarreras.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnCarreras.Location = new System.Drawing.Point(0, 110);
            this.btnCarreras.Name = "btnCarreras";
            this.btnCarreras.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnCarreras.Size = new System.Drawing.Size(210, 50);
            this.btnCarreras.TabIndex = 2;
            this.btnCarreras.Text = "🎓  Carreras";
            this.btnCarreras.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCarreras.UseVisualStyleBackColor = true;
            this.btnCarreras.Click += new System.EventHandler(this.btnCarreras_Click);
            // 
            // btnInicio
            // 
            this.btnInicio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInicio.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnInicio.FlatAppearance.BorderSize = 0;
            this.btnInicio.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            this.btnInicio.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(253)))), ((int)(((byte)(244)))));
            this.btnInicio.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInicio.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInicio.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnInicio.Location = new System.Drawing.Point(0, 60);
            this.btnInicio.Name = "btnInicio";
            this.btnInicio.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnInicio.Size = new System.Drawing.Size(210, 50);
            this.btnInicio.TabIndex = 1;
            this.btnInicio.Text = "🏠  Inicio";
            this.btnInicio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInicio.UseVisualStyleBackColor = true;
            this.btnInicio.Click += new System.EventHandler(this.btnInicio_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(133)))), ((int)(((byte)(66)))));
            this.panelLogo.Controls.Add(this.btnMenuLateral);
            this.panelLogo.Controls.Add(this.lblTituloApp);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(210, 48);
            this.panelLogo.TabIndex = 0;
            // 
            // btnMenuLateral
            // 
            this.btnMenuLateral.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenuLateral.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMenuLateral.FlatAppearance.BorderSize = 0;
            this.btnMenuLateral.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(83)))), ((int)(((byte)(45)))));
            this.btnMenuLateral.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnMenuLateral.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenuLateral.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenuLateral.ForeColor = System.Drawing.Color.White;
            this.btnMenuLateral.Location = new System.Drawing.Point(165, 0);
            this.btnMenuLateral.Name = "btnMenuLateral";
            this.btnMenuLateral.Size = new System.Drawing.Size(45, 48);
            this.btnMenuLateral.TabIndex = 1;
            this.btnMenuLateral.Text = "≡";
            this.btnMenuLateral.UseVisualStyleBackColor = true;
            this.btnMenuLateral.Click += new System.EventHandler(this.btnMenuLateral_Click);
            // 
            // lblTituloApp
            // 
            this.lblTituloApp.AutoSize = true;
            this.lblTituloApp.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloApp.ForeColor = System.Drawing.Color.White;
            this.lblTituloApp.Location = new System.Drawing.Point(12, 14);
            this.lblTituloApp.Name = "lblTituloApp";
            this.lblTituloApp.Size = new System.Drawing.Size(107, 20);
            this.lblTituloApp.TabIndex = 0;
            this.lblTituloApp.Text = "⭐ Rally ULSA";
            // 
            // panelTopHeader
            // 
            this.panelTopHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(133)))), ((int)(((byte)(66)))));
            this.panelTopHeader.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.panelTopHeader.Controls.Add(this.lblUsuarioSesion);
            this.panelTopHeader.Controls.Add(this.lblEvento);
            this.panelTopHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTopHeader.Location = new System.Drawing.Point(210, 0);
            this.panelTopHeader.Name = "panelTopHeader";
            this.panelTopHeader.Padding = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.panelTopHeader.Size = new System.Drawing.Size(890, 48);
            this.panelTopHeader.TabIndex = 1;
            // 
            // lblUsuarioSesion
            // 
            this.lblUsuarioSesion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUsuarioSesion.AutoSize = true;
            this.lblUsuarioSesion.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuarioSesion.ForeColor = System.Drawing.Color.White;
            this.lblUsuarioSesion.Location = new System.Drawing.Point(620, 15);
            this.lblUsuarioSesion.Name = "lblUsuarioSesion";
            this.lblUsuarioSesion.Size = new System.Drawing.Size(145, 17);
            this.lblUsuarioSesion.TabIndex = 1;
            this.lblUsuarioSesion.Text = "👤 Organizador: Admin";
            // 
            // lblEvento
            // 
            this.lblEvento.AutoSize = true;
            this.lblEvento.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEvento.ForeColor = System.Drawing.Color.White;
            this.lblEvento.Location = new System.Drawing.Point(15, 14);
            this.lblEvento.Name = "lblEvento";
            this.lblEvento.Size = new System.Drawing.Size(320, 19);
            this.lblEvento.TabIndex = 0;
            this.lblEvento.Text = "Sistema de Gestión de Rally Universitario ULSA";
            // 
            // panelContenidoPrincipal
            // 
            this.panelContenidoPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContenidoPrincipal.Location = new System.Drawing.Point(210, 45);
            this.panelContenidoPrincipal.Name = "panelContenidoPrincipal";
            this.panelContenidoPrincipal.Size = new System.Drawing.Size(890, 636);
            this.panelContenidoPrincipal.TabIndex = 2;
            // 
            // PaginaInicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1100, 681);
            this.Controls.Add(this.panelContenidoPrincipal);
            this.Controls.Add(this.panelTopHeader);
            this.Controls.Add(this.panelMenuLateral);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(950, 600);
            this.Name = "PaginaInicio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sistema de Gestión de Rally Universitario - ULSA";
            this.Load += new System.EventHandler(this.PaginaInicio_Load);
            this.panelMenuLateral.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panelLogo.PerformLayout();
            this.panelTopHeader.ResumeLayout(false);
            this.panelTopHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenuLateral;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.Label lblTituloApp;
        private System.Windows.Forms.Button btnMenuLateral;
        private System.Windows.Forms.Button btnInicio;
        private System.Windows.Forms.Button btnCarreras;
        private System.Windows.Forms.Button btnEstudiantes;
        private System.Windows.Forms.Button btnRetos;
        private System.Windows.Forms.Button btnResultados;
        private System.Windows.Forms.Button btnClasificacion;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Panel panelTopHeader;
        private System.Windows.Forms.Label lblEvento;
        private System.Windows.Forms.Label lblUsuarioSesion;
        private System.Windows.Forms.Panel panelContenidoPrincipal;
    }
}
