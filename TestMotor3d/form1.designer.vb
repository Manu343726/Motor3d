<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.Pic = New System.Windows.Forms.PictureBox
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.ArchivoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ConfiguracionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DatosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator
        Me.ReiniciarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PoliedrosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AñadirPoliedroToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CuboToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PiramideToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FalsoConToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AnilloToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.IcosaedroToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DodecaedroToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CilindroToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EsferaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.QuitarPoliedroToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FocosToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AñadirFocoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.QuitarFocoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator
        Me.MoverFocosConElCursorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EstablecerPosiciónDeUnFocoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CambiarColorFocoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CambiarTipoFocoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PropiedadesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DirecciónToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DeltaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RadioToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SeleccionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SeleccionarPoliedroToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.TrasladarToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EscalarPoliedroToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RotarPoliedroToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.TrasladaraToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EscalarFijoToolStripMenuItem3 = New System.Windows.Forms.ToolStripMenuItem
        Me.RotarAlrededorToolStripMenuItem2 = New System.Windows.Forms.ToolStripMenuItem
        Me.ReestablecerTransformacionesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.CambiarColorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MovimientoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.PararReanudarMovimientoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.VisualizacionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ModoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.WireFrameShadingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.FlatShadingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.GouraudShadingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DepuracionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MostrarNormalesDeLasCarasToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MostrarNormalesDeLosVértiToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MostrarDatosDeLosVérticesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MostrarTrianToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MostrarCarabufferSeñaladaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MostrarIndicesBufferToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator
        Me.SalidaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.BufferToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DibujoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SombreadoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TodoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.RepresentacionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OrtogonalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.XOYToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ZOXToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ZOYToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OrtogonalGeneralToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OrtogonalLibreToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.IsometricaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ConicaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        Me.PosiciónDeLaCámaraToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DireccionCamaraToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DistanciaDelPlanoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.SombrearToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ActivadoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator
        Me.ConstantesDeDistanciaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ExponenteDeReflexiónEspecularToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator
        Me.ComponentesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LuzAmbientalToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ReflexiónDifusaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ReflexiónEspecularToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.IntensidadesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LuzAmbientalToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ReflexiónDifusaToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.ReflexiónEspecularToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.DibujarEjesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.CalculoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DibujarLucesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip
        Me.Label1 = New System.Windows.Forms.ToolStripStatusLabel
        Me.ColorDialog = New System.Windows.Forms.ColorDialog
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator
        Me.TeselacionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        CType(Me.Pic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Pic
        '
        Me.Pic.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Pic.Location = New System.Drawing.Point(9, 28)
        Me.Pic.Name = "Pic"
        Me.Pic.Size = New System.Drawing.Size(379, 252)
        Me.Pic.TabIndex = 0
        Me.Pic.TabStop = False
        '
        'Timer1
        '
        Me.Timer1.Interval = 1
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArchivoToolStripMenuItem, Me.PoliedrosToolStripMenuItem, Me.FocosToolStripMenuItem, Me.SeleccionToolStripMenuItem, Me.MovimientoToolStripMenuItem, Me.VisualizacionToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(400, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'ArchivoToolStripMenuItem
        '
        Me.ArchivoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ConfiguracionToolStripMenuItem, Me.DatosToolStripMenuItem, Me.ToolStripSeparator10, Me.ReiniciarToolStripMenuItem, Me.SaToolStripMenuItem})
        Me.ArchivoToolStripMenuItem.Name = "ArchivoToolStripMenuItem"
        Me.ArchivoToolStripMenuItem.Size = New System.Drawing.Size(55, 20)
        Me.ArchivoToolStripMenuItem.Text = "Archivo"
        '
        'ConfiguracionToolStripMenuItem
        '
        Me.ConfiguracionToolStripMenuItem.Name = "ConfiguracionToolStripMenuItem"
        Me.ConfiguracionToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ConfiguracionToolStripMenuItem.Text = "Configuración..."
        '
        'DatosToolStripMenuItem
        '
        Me.DatosToolStripMenuItem.Name = "DatosToolStripMenuItem"
        Me.DatosToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.DatosToolStripMenuItem.Text = "Datos..."
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(149, 6)
        '
        'ReiniciarToolStripMenuItem
        '
        Me.ReiniciarToolStripMenuItem.Name = "ReiniciarToolStripMenuItem"
        Me.ReiniciarToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.ReiniciarToolStripMenuItem.Text = "Reiniciar"
        '
        'SaToolStripMenuItem
        '
        Me.SaToolStripMenuItem.Name = "SaToolStripMenuItem"
        Me.SaToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.SaToolStripMenuItem.Text = "Salir"
        '
        'PoliedrosToolStripMenuItem
        '
        Me.PoliedrosToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AñadirPoliedroToolStripMenuItem, Me.QuitarPoliedroToolStripMenuItem})
        Me.PoliedrosToolStripMenuItem.Name = "PoliedrosToolStripMenuItem"
        Me.PoliedrosToolStripMenuItem.Size = New System.Drawing.Size(62, 20)
        Me.PoliedrosToolStripMenuItem.Text = "Poliedros"
        '
        'AñadirPoliedroToolStripMenuItem
        '
        Me.AñadirPoliedroToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CuboToolStripMenuItem, Me.PiramideToolStripMenuItem, Me.FalsoConToolStripMenuItem, Me.AnilloToolStripMenuItem, Me.IcosaedroToolStripMenuItem, Me.DodecaedroToolStripMenuItem, Me.CilindroToolStripMenuItem, Me.EsferaToolStripMenuItem})
        Me.AñadirPoliedroToolStripMenuItem.Name = "AñadirPoliedroToolStripMenuItem"
        Me.AñadirPoliedroToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.AñadirPoliedroToolStripMenuItem.Text = "Nuevo poliedro"
        '
        'CuboToolStripMenuItem
        '
        Me.CuboToolStripMenuItem.Name = "CuboToolStripMenuItem"
        Me.CuboToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.CuboToolStripMenuItem.Text = "Cubo"
        '
        'PiramideToolStripMenuItem
        '
        Me.PiramideToolStripMenuItem.Name = "PiramideToolStripMenuItem"
        Me.PiramideToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.PiramideToolStripMenuItem.Text = "Piramide"
        '
        'FalsoConToolStripMenuItem
        '
        Me.FalsoConToolStripMenuItem.Name = "FalsoConToolStripMenuItem"
        Me.FalsoConToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.FalsoConToolStripMenuItem.Text = "FalsoCono"
        '
        'AnilloToolStripMenuItem
        '
        Me.AnilloToolStripMenuItem.Name = "AnilloToolStripMenuItem"
        Me.AnilloToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.AnilloToolStripMenuItem.Text = "Anillo"
        '
        'IcosaedroToolStripMenuItem
        '
        Me.IcosaedroToolStripMenuItem.Name = "IcosaedroToolStripMenuItem"
        Me.IcosaedroToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.IcosaedroToolStripMenuItem.Text = "Icosaedro"
        '
        'DodecaedroToolStripMenuItem
        '
        Me.DodecaedroToolStripMenuItem.Name = "DodecaedroToolStripMenuItem"
        Me.DodecaedroToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.DodecaedroToolStripMenuItem.Text = "Dodecaedro"
        '
        'CilindroToolStripMenuItem
        '
        Me.CilindroToolStripMenuItem.Name = "CilindroToolStripMenuItem"
        Me.CilindroToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.CilindroToolStripMenuItem.Text = "Cilindro"
        '
        'EsferaToolStripMenuItem
        '
        Me.EsferaToolStripMenuItem.Name = "EsferaToolStripMenuItem"
        Me.EsferaToolStripMenuItem.Size = New System.Drawing.Size(132, 22)
        Me.EsferaToolStripMenuItem.Text = "Esfera"
        '
        'QuitarPoliedroToolStripMenuItem
        '
        Me.QuitarPoliedroToolStripMenuItem.Name = "QuitarPoliedroToolStripMenuItem"
        Me.QuitarPoliedroToolStripMenuItem.Size = New System.Drawing.Size(146, 22)
        Me.QuitarPoliedroToolStripMenuItem.Text = "Quitar poliedro"
        '
        'FocosToolStripMenuItem
        '
        Me.FocosToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AñadirFocoToolStripMenuItem, Me.QuitarFocoToolStripMenuItem, Me.ToolStripSeparator7, Me.MoverFocosConElCursorToolStripMenuItem, Me.EstablecerPosiciónDeUnFocoToolStripMenuItem, Me.CambiarColorFocoToolStripMenuItem, Me.CambiarTipoFocoToolStripMenuItem, Me.PropiedadesToolStripMenuItem})
        Me.FocosToolStripMenuItem.Name = "FocosToolStripMenuItem"
        Me.FocosToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.FocosToolStripMenuItem.Text = "Focos"
        '
        'AñadirFocoToolStripMenuItem
        '
        Me.AñadirFocoToolStripMenuItem.Name = "AñadirFocoToolStripMenuItem"
        Me.AñadirFocoToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.AñadirFocoToolStripMenuItem.Text = "Añadir foco"
        '
        'QuitarFocoToolStripMenuItem
        '
        Me.QuitarFocoToolStripMenuItem.Name = "QuitarFocoToolStripMenuItem"
        Me.QuitarFocoToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.QuitarFocoToolStripMenuItem.Text = "Quitar foco"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(194, 6)
        '
        'MoverFocosConElCursorToolStripMenuItem
        '
        Me.MoverFocosConElCursorToolStripMenuItem.Name = "MoverFocosConElCursorToolStripMenuItem"
        Me.MoverFocosConElCursorToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.MoverFocosConElCursorToolStripMenuItem.Text = "Mover focos con el cursor"
        '
        'EstablecerPosiciónDeUnFocoToolStripMenuItem
        '
        Me.EstablecerPosiciónDeUnFocoToolStripMenuItem.Name = "EstablecerPosiciónDeUnFocoToolStripMenuItem"
        Me.EstablecerPosiciónDeUnFocoToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.EstablecerPosiciónDeUnFocoToolStripMenuItem.Text = "Recolocar foco..."
        '
        'CambiarColorFocoToolStripMenuItem
        '
        Me.CambiarColorFocoToolStripMenuItem.Name = "CambiarColorFocoToolStripMenuItem"
        Me.CambiarColorFocoToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.CambiarColorFocoToolStripMenuItem.Text = "Cambiar color foco..."
        '
        'CambiarTipoFocoToolStripMenuItem
        '
        Me.CambiarTipoFocoToolStripMenuItem.Name = "CambiarTipoFocoToolStripMenuItem"
        Me.CambiarTipoFocoToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.CambiarTipoFocoToolStripMenuItem.Text = "Cambiar tipo foco..."
        '
        'PropiedadesToolStripMenuItem
        '
        Me.PropiedadesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DirecciónToolStripMenuItem, Me.DeltaToolStripMenuItem, Me.RadioToolStripMenuItem})
        Me.PropiedadesToolStripMenuItem.Name = "PropiedadesToolStripMenuItem"
        Me.PropiedadesToolStripMenuItem.Size = New System.Drawing.Size(197, 22)
        Me.PropiedadesToolStripMenuItem.Text = "Propiedades..."
        '
        'DirecciónToolStripMenuItem
        '
        Me.DirecciónToolStripMenuItem.Name = "DirecciónToolStripMenuItem"
        Me.DirecciónToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.DirecciónToolStripMenuItem.Text = "Dirección..."
        '
        'DeltaToolStripMenuItem
        '
        Me.DeltaToolStripMenuItem.Name = "DeltaToolStripMenuItem"
        Me.DeltaToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.DeltaToolStripMenuItem.Text = "Delta..."
        '
        'RadioToolStripMenuItem
        '
        Me.RadioToolStripMenuItem.Name = "RadioToolStripMenuItem"
        Me.RadioToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.RadioToolStripMenuItem.Text = "Radio..."
        '
        'SeleccionToolStripMenuItem
        '
        Me.SeleccionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SeleccionarPoliedroToolStripMenuItem, Me.ToolStripSeparator1, Me.TrasladarToolStripMenuItem, Me.EscalarPoliedroToolStripMenuItem, Me.RotarPoliedroToolStripMenuItem, Me.ToolStripSeparator2, Me.TrasladaraToolStripMenuItem, Me.EscalarFijoToolStripMenuItem3, Me.RotarAlrededorToolStripMenuItem2, Me.ReestablecerTransformacionesToolStripMenuItem, Me.ToolStripSeparator4, Me.CambiarColorToolStripMenuItem})
        Me.SeleccionToolStripMenuItem.Name = "SeleccionToolStripMenuItem"
        Me.SeleccionToolStripMenuItem.Size = New System.Drawing.Size(63, 20)
        Me.SeleccionToolStripMenuItem.Text = "Seleccion"
        '
        'SeleccionarPoliedroToolStripMenuItem
        '
        Me.SeleccionarPoliedroToolStripMenuItem.Name = "SeleccionarPoliedroToolStripMenuItem"
        Me.SeleccionarPoliedroToolStripMenuItem.Size = New System.Drawing.Size(223, 22)
        Me.SeleccionarPoliedroToolStripMenuItem.Text = "Seleccionar poliedro"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(220, 6)
        '
        'TrasladarToolStripMenuItem
        '
        Me.TrasladarToolStripMenuItem.Name = "TrasladarToolStripMenuItem"
        Me.TrasladarToolStripMenuItem.Size = New System.Drawing.Size(223, 22)
        Me.TrasladarToolStripMenuItem.Text = "Trasladar poliedro"
        '
        'EscalarPoliedroToolStripMenuItem
        '
        Me.EscalarPoliedroToolStripMenuItem.Name = "EscalarPoliedroToolStripMenuItem"
        Me.EscalarPoliedroToolStripMenuItem.Size = New System.Drawing.Size(223, 22)
        Me.EscalarPoliedroToolStripMenuItem.Text = "Escalar poliedro"
        '
        'RotarPoliedroToolStripMenuItem
        '
        Me.RotarPoliedroToolStripMenuItem.Name = "RotarPoliedroToolStripMenuItem"
        Me.RotarPoliedroToolStripMenuItem.Size = New System.Drawing.Size(223, 22)
        Me.RotarPoliedroToolStripMenuItem.Text = "Rotar poliedro"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(220, 6)
        '
        'TrasladaraToolStripMenuItem
        '
        Me.TrasladaraToolStripMenuItem.Name = "TrasladaraToolStripMenuItem"
        Me.TrasladaraToolStripMenuItem.Size = New System.Drawing.Size(223, 22)
        Me.TrasladaraToolStripMenuItem.Text = "Trasladar a..."
        '
        'EscalarFijoToolStripMenuItem3
        '
        Me.EscalarFijoToolStripMenuItem3.Name = "EscalarFijoToolStripMenuItem3"
        Me.EscalarFijoToolStripMenuItem3.Size = New System.Drawing.Size(223, 22)
        Me.EscalarFijoToolStripMenuItem3.Text = "Escalar (Fijo)..."
        '
        'RotarAlrededorToolStripMenuItem2
        '
        Me.RotarAlrededorToolStripMenuItem2.Name = "RotarAlrededorToolStripMenuItem2"
        Me.RotarAlrededorToolStripMenuItem2.Size = New System.Drawing.Size(223, 22)
        Me.RotarAlrededorToolStripMenuItem2.Text = "Rotar sobre si mismo..."
        '
        'ReestablecerTransformacionesToolStripMenuItem
        '
        Me.ReestablecerTransformacionesToolStripMenuItem.Name = "ReestablecerTransformacionesToolStripMenuItem"
        Me.ReestablecerTransformacionesToolStripMenuItem.Size = New System.Drawing.Size(223, 22)
        Me.ReestablecerTransformacionesToolStripMenuItem.Text = "Reestablecer transformaciones"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(220, 6)
        '
        'CambiarColorToolStripMenuItem
        '
        Me.CambiarColorToolStripMenuItem.Name = "CambiarColorToolStripMenuItem"
        Me.CambiarColorToolStripMenuItem.Size = New System.Drawing.Size(223, 22)
        Me.CambiarColorToolStripMenuItem.Text = "Cambiar color..."
        '
        'MovimientoToolStripMenuItem
        '
        Me.MovimientoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PararReanudarMovimientoToolStripMenuItem})
        Me.MovimientoToolStripMenuItem.Name = "MovimientoToolStripMenuItem"
        Me.MovimientoToolStripMenuItem.Size = New System.Drawing.Size(73, 20)
        Me.MovimientoToolStripMenuItem.Text = "Movimiento"
        '
        'PararReanudarMovimientoToolStripMenuItem
        '
        Me.PararReanudarMovimientoToolStripMenuItem.Name = "PararReanudarMovimientoToolStripMenuItem"
        Me.PararReanudarMovimientoToolStripMenuItem.Size = New System.Drawing.Size(214, 22)
        Me.PararReanudarMovimientoToolStripMenuItem.Text = "Parar / Reanudar movimiento"
        '
        'VisualizacionToolStripMenuItem
        '
        Me.VisualizacionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ModoToolStripMenuItem, Me.DepuracionToolStripMenuItem, Me.ToolStripSeparator3, Me.RepresentacionToolStripMenuItem, Me.ToolStripSeparator5, Me.SombrearToolStripMenuItem, Me.DibujarEjesToolStripMenuItem, Me.CalculoToolStripMenuItem, Me.DibujarLucesToolStripMenuItem})
        Me.VisualizacionToolStripMenuItem.Name = "VisualizacionToolStripMenuItem"
        Me.VisualizacionToolStripMenuItem.Size = New System.Drawing.Size(47, 20)
        Me.VisualizacionToolStripMenuItem.Text = "Motor"
        '
        'ModoToolStripMenuItem
        '
        Me.ModoToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.WireFrameShadingToolStripMenuItem, Me.FlatShadingToolStripMenuItem, Me.GouraudShadingToolStripMenuItem, Me.ToolStripSeparator12, Me.TeselacionToolStripMenuItem})
        Me.ModoToolStripMenuItem.Name = "ModoToolStripMenuItem"
        Me.ModoToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.ModoToolStripMenuItem.Text = "Modo"
        '
        'WireFrameShadingToolStripMenuItem
        '
        Me.WireFrameShadingToolStripMenuItem.Name = "WireFrameShadingToolStripMenuItem"
        Me.WireFrameShadingToolStripMenuItem.Size = New System.Drawing.Size(268, 22)
        Me.WireFrameShadingToolStripMenuItem.Text = "WireFrame shading (Figuras de alambre)"
        '
        'FlatShadingToolStripMenuItem
        '
        Me.FlatShadingToolStripMenuItem.Name = "FlatShadingToolStripMenuItem"
        Me.FlatShadingToolStripMenuItem.Size = New System.Drawing.Size(268, 22)
        Me.FlatShadingToolStripMenuItem.Text = "Flat shading"
        '
        'GouraudShadingToolStripMenuItem
        '
        Me.GouraudShadingToolStripMenuItem.Name = "GouraudShadingToolStripMenuItem"
        Me.GouraudShadingToolStripMenuItem.Size = New System.Drawing.Size(268, 22)
        Me.GouraudShadingToolStripMenuItem.Text = "Gouraud shading"
        '
        'DepuracionToolStripMenuItem
        '
        Me.DepuracionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MostrarNormalesDeLasCarasToolStripMenuItem, Me.MostrarNormalesDeLosVértiToolStripMenuItem, Me.MostrarDatosDeLosVérticesToolStripMenuItem, Me.MostrarTrianToolStripMenuItem, Me.MostrarCarabufferSeñaladaToolStripMenuItem, Me.MostrarIndicesBufferToolStripMenuItem, Me.ToolStripSeparator11, Me.SalidaToolStripMenuItem})
        Me.DepuracionToolStripMenuItem.Name = "DepuracionToolStripMenuItem"
        Me.DepuracionToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.DepuracionToolStripMenuItem.Text = "Depuración"
        '
        'MostrarNormalesDeLasCarasToolStripMenuItem
        '
        Me.MostrarNormalesDeLasCarasToolStripMenuItem.Name = "MostrarNormalesDeLasCarasToolStripMenuItem"
        Me.MostrarNormalesDeLasCarasToolStripMenuItem.Size = New System.Drawing.Size(229, 22)
        Me.MostrarNormalesDeLasCarasToolStripMenuItem.Text = "Mostrar normales de las caras"
        '
        'MostrarNormalesDeLosVértiToolStripMenuItem
        '
        Me.MostrarNormalesDeLosVértiToolStripMenuItem.Name = "MostrarNormalesDeLosVértiToolStripMenuItem"
        Me.MostrarNormalesDeLosVértiToolStripMenuItem.Size = New System.Drawing.Size(229, 22)
        Me.MostrarNormalesDeLosVértiToolStripMenuItem.Text = "Mostrar normales de los vértices"
        '
        'MostrarDatosDeLosVérticesToolStripMenuItem
        '
        Me.MostrarDatosDeLosVérticesToolStripMenuItem.Name = "MostrarDatosDeLosVérticesToolStripMenuItem"
        Me.MostrarDatosDeLosVérticesToolStripMenuItem.Size = New System.Drawing.Size(229, 22)
        Me.MostrarDatosDeLosVérticesToolStripMenuItem.Text = "Mostrar datos de los vértices"
        '
        'MostrarTrianToolStripMenuItem
        '
        Me.MostrarTrianToolStripMenuItem.Name = "MostrarTrianToolStripMenuItem"
        Me.MostrarTrianToolStripMenuItem.Size = New System.Drawing.Size(229, 22)
        Me.MostrarTrianToolStripMenuItem.Text = "Mostrar triangulación"
        '
        'MostrarCarabufferSeñaladaToolStripMenuItem
        '
        Me.MostrarCarabufferSeñaladaToolStripMenuItem.Name = "MostrarCarabufferSeñaladaToolStripMenuItem"
        Me.MostrarCarabufferSeñaladaToolStripMenuItem.Size = New System.Drawing.Size(229, 22)
        Me.MostrarCarabufferSeñaladaToolStripMenuItem.Text = "Mostrar carabuffer señalada"
        '
        'MostrarIndicesBufferToolStripMenuItem
        '
        Me.MostrarIndicesBufferToolStripMenuItem.Name = "MostrarIndicesBufferToolStripMenuItem"
        Me.MostrarIndicesBufferToolStripMenuItem.Size = New System.Drawing.Size(229, 22)
        Me.MostrarIndicesBufferToolStripMenuItem.Text = "Mostrar índices buffer"
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(226, 6)
        '
        'SalidaToolStripMenuItem
        '
        Me.SalidaToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BufferToolStripMenuItem, Me.DibujoToolStripMenuItem, Me.SombreadoToolStripMenuItem, Me.TodoToolStripMenuItem})
        Me.SalidaToolStripMenuItem.Name = "SalidaToolStripMenuItem"
        Me.SalidaToolStripMenuItem.Size = New System.Drawing.Size(229, 22)
        Me.SalidaToolStripMenuItem.Text = "Salida..."
        '
        'BufferToolStripMenuItem
        '
        Me.BufferToolStripMenuItem.Name = "BufferToolStripMenuItem"
        Me.BufferToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.BufferToolStripMenuItem.Text = "Buffer"
        '
        'DibujoToolStripMenuItem
        '
        Me.DibujoToolStripMenuItem.Name = "DibujoToolStripMenuItem"
        Me.DibujoToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.DibujoToolStripMenuItem.Text = "Dibujo"
        '
        'SombreadoToolStripMenuItem
        '
        Me.SombreadoToolStripMenuItem.Name = "SombreadoToolStripMenuItem"
        Me.SombreadoToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.SombreadoToolStripMenuItem.Text = "Sombreado"
        '
        'TodoToolStripMenuItem
        '
        Me.TodoToolStripMenuItem.Name = "TodoToolStripMenuItem"
        Me.TodoToolStripMenuItem.Size = New System.Drawing.Size(128, 22)
        Me.TodoToolStripMenuItem.Text = "Todo"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(158, 6)
        '
        'RepresentacionToolStripMenuItem
        '
        Me.RepresentacionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OrtogonalToolStripMenuItem, Me.OrtogonalGeneralToolStripMenuItem, Me.OrtogonalLibreToolStripMenuItem, Me.IsometricaToolStripMenuItem, Me.ConicaToolStripMenuItem, Me.ToolStripSeparator6, Me.PosiciónDeLaCámaraToolStripMenuItem, Me.DireccionCamaraToolStripMenuItem, Me.DistanciaDelPlanoToolStripMenuItem})
        Me.RepresentacionToolStripMenuItem.Name = "RepresentacionToolStripMenuItem"
        Me.RepresentacionToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.RepresentacionToolStripMenuItem.Text = "Representación..."
        '
        'OrtogonalToolStripMenuItem
        '
        Me.OrtogonalToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.XOYToolStripMenuItem, Me.ZOXToolStripMenuItem, Me.ZOYToolStripMenuItem})
        Me.OrtogonalToolStripMenuItem.Name = "OrtogonalToolStripMenuItem"
        Me.OrtogonalToolStripMenuItem.Size = New System.Drawing.Size(245, 22)
        Me.OrtogonalToolStripMenuItem.Text = "Ortogonal"
        '
        'XOYToolStripMenuItem
        '
        Me.XOYToolStripMenuItem.Name = "XOYToolStripMenuItem"
        Me.XOYToolStripMenuItem.Size = New System.Drawing.Size(94, 22)
        Me.XOYToolStripMenuItem.Text = "XOY"
        '
        'ZOXToolStripMenuItem
        '
        Me.ZOXToolStripMenuItem.Name = "ZOXToolStripMenuItem"
        Me.ZOXToolStripMenuItem.Size = New System.Drawing.Size(94, 22)
        Me.ZOXToolStripMenuItem.Text = "ZOX"
        '
        'ZOYToolStripMenuItem
        '
        Me.ZOYToolStripMenuItem.Name = "ZOYToolStripMenuItem"
        Me.ZOYToolStripMenuItem.Size = New System.Drawing.Size(94, 22)
        Me.ZOYToolStripMenuItem.Text = "ZOY"
        '
        'OrtogonalGeneralToolStripMenuItem
        '
        Me.OrtogonalGeneralToolStripMenuItem.Name = "OrtogonalGeneralToolStripMenuItem"
        Me.OrtogonalGeneralToolStripMenuItem.Size = New System.Drawing.Size(245, 22)
        Me.OrtogonalGeneralToolStripMenuItem.Text = "Ortogonal general"
        '
        'OrtogonalLibreToolStripMenuItem
        '
        Me.OrtogonalLibreToolStripMenuItem.Name = "OrtogonalLibreToolStripMenuItem"
        Me.OrtogonalLibreToolStripMenuItem.Size = New System.Drawing.Size(245, 22)
        Me.OrtogonalLibreToolStripMenuItem.Text = "Ortogonal libre"
        '
        'IsometricaToolStripMenuItem
        '
        Me.IsometricaToolStripMenuItem.Name = "IsometricaToolStripMenuItem"
        Me.IsometricaToolStripMenuItem.Size = New System.Drawing.Size(245, 22)
        Me.IsometricaToolStripMenuItem.Text = "Isometrica"
        '
        'ConicaToolStripMenuItem
        '
        Me.ConicaToolStripMenuItem.Name = "ConicaToolStripMenuItem"
        Me.ConicaToolStripMenuItem.Size = New System.Drawing.Size(245, 22)
        Me.ConicaToolStripMenuItem.Text = "Conica"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(242, 6)
        '
        'PosiciónDeLaCámaraToolStripMenuItem
        '
        Me.PosiciónDeLaCámaraToolStripMenuItem.Enabled = False
        Me.PosiciónDeLaCámaraToolStripMenuItem.Name = "PosiciónDeLaCámaraToolStripMenuItem"
        Me.PosiciónDeLaCámaraToolStripMenuItem.Size = New System.Drawing.Size(245, 22)
        Me.PosiciónDeLaCámaraToolStripMenuItem.Text = "Posición de la cámara..."
        '
        'DireccionCamaraToolStripMenuItem
        '
        Me.DireccionCamaraToolStripMenuItem.Name = "DireccionCamaraToolStripMenuItem"
        Me.DireccionCamaraToolStripMenuItem.Size = New System.Drawing.Size(245, 22)
        Me.DireccionCamaraToolStripMenuItem.Text = "Dirección de la cámara..."
        '
        'DistanciaDelPlanoToolStripMenuItem
        '
        Me.DistanciaDelPlanoToolStripMenuItem.Name = "DistanciaDelPlanoToolStripMenuItem"
        Me.DistanciaDelPlanoToolStripMenuItem.Size = New System.Drawing.Size(245, 22)
        Me.DistanciaDelPlanoToolStripMenuItem.Text = "Distancia del plano de proyección..."
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(158, 6)
        '
        'SombrearToolStripMenuItem
        '
        Me.SombrearToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ActivadoToolStripMenuItem, Me.ToolStripSeparator8, Me.ConstantesDeDistanciaToolStripMenuItem, Me.ExponenteDeReflexiónEspecularToolStripMenuItem, Me.ToolStripSeparator9, Me.ComponentesToolStripMenuItem, Me.IntensidadesToolStripMenuItem})
        Me.SombrearToolStripMenuItem.Name = "SombrearToolStripMenuItem"
        Me.SombrearToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.SombrearToolStripMenuItem.Text = "Sombreado..."
        '
        'ActivadoToolStripMenuItem
        '
        Me.ActivadoToolStripMenuItem.Name = "ActivadoToolStripMenuItem"
        Me.ActivadoToolStripMenuItem.Size = New System.Drawing.Size(247, 22)
        Me.ActivadoToolStripMenuItem.Text = "Activado"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(244, 6)
        '
        'ConstantesDeDistanciaToolStripMenuItem
        '
        Me.ConstantesDeDistanciaToolStripMenuItem.Name = "ConstantesDeDistanciaToolStripMenuItem"
        Me.ConstantesDeDistanciaToolStripMenuItem.Size = New System.Drawing.Size(247, 22)
        Me.ConstantesDeDistanciaToolStripMenuItem.Text = "Constantes de distancia..."
        '
        'ExponenteDeReflexiónEspecularToolStripMenuItem
        '
        Me.ExponenteDeReflexiónEspecularToolStripMenuItem.Name = "ExponenteDeReflexiónEspecularToolStripMenuItem"
        Me.ExponenteDeReflexiónEspecularToolStripMenuItem.Size = New System.Drawing.Size(247, 22)
        Me.ExponenteDeReflexiónEspecularToolStripMenuItem.Text = "Exponente de reflexión especular..."
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(244, 6)
        '
        'ComponentesToolStripMenuItem
        '
        Me.ComponentesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LuzAmbientalToolStripMenuItem, Me.ReflexiónDifusaToolStripMenuItem, Me.ReflexiónEspecularToolStripMenuItem})
        Me.ComponentesToolStripMenuItem.Name = "ComponentesToolStripMenuItem"
        Me.ComponentesToolStripMenuItem.Size = New System.Drawing.Size(247, 22)
        Me.ComponentesToolStripMenuItem.Text = "Componentes..."
        '
        'LuzAmbientalToolStripMenuItem
        '
        Me.LuzAmbientalToolStripMenuItem.Name = "LuzAmbientalToolStripMenuItem"
        Me.LuzAmbientalToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.LuzAmbientalToolStripMenuItem.Text = "Luz ambiental..."
        '
        'ReflexiónDifusaToolStripMenuItem
        '
        Me.ReflexiónDifusaToolStripMenuItem.Name = "ReflexiónDifusaToolStripMenuItem"
        Me.ReflexiónDifusaToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ReflexiónDifusaToolStripMenuItem.Text = "Reflexión difusa..."
        '
        'ReflexiónEspecularToolStripMenuItem
        '
        Me.ReflexiónEspecularToolStripMenuItem.Name = "ReflexiónEspecularToolStripMenuItem"
        Me.ReflexiónEspecularToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ReflexiónEspecularToolStripMenuItem.Text = "Reflexión especular..."
        '
        'IntensidadesToolStripMenuItem
        '
        Me.IntensidadesToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LuzAmbientalToolStripMenuItem1, Me.ReflexiónDifusaToolStripMenuItem1, Me.ReflexiónEspecularToolStripMenuItem1})
        Me.IntensidadesToolStripMenuItem.Name = "IntensidadesToolStripMenuItem"
        Me.IntensidadesToolStripMenuItem.Size = New System.Drawing.Size(247, 22)
        Me.IntensidadesToolStripMenuItem.Text = "Intensidades..."
        '
        'LuzAmbientalToolStripMenuItem1
        '
        Me.LuzAmbientalToolStripMenuItem1.Name = "LuzAmbientalToolStripMenuItem1"
        Me.LuzAmbientalToolStripMenuItem1.Size = New System.Drawing.Size(180, 22)
        Me.LuzAmbientalToolStripMenuItem1.Text = "Luz ambiental..."
        '
        'ReflexiónDifusaToolStripMenuItem1
        '
        Me.ReflexiónDifusaToolStripMenuItem1.Name = "ReflexiónDifusaToolStripMenuItem1"
        Me.ReflexiónDifusaToolStripMenuItem1.Size = New System.Drawing.Size(180, 22)
        Me.ReflexiónDifusaToolStripMenuItem1.Text = "Reflexión difusa..."
        '
        'ReflexiónEspecularToolStripMenuItem1
        '
        Me.ReflexiónEspecularToolStripMenuItem1.Name = "ReflexiónEspecularToolStripMenuItem1"
        Me.ReflexiónEspecularToolStripMenuItem1.Size = New System.Drawing.Size(180, 22)
        Me.ReflexiónEspecularToolStripMenuItem1.Text = "Reflexión especular..."
        '
        'DibujarEjesToolStripMenuItem
        '
        Me.DibujarEjesToolStripMenuItem.Name = "DibujarEjesToolStripMenuItem"
        Me.DibujarEjesToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.DibujarEjesToolStripMenuItem.Text = "Dibujar ejes"
        '
        'CalculoToolStripMenuItem
        '
        Me.CalculoToolStripMenuItem.Name = "CalculoToolStripMenuItem"
        Me.CalculoToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.CalculoToolStripMenuItem.Text = "Calcular colisiones"
        '
        'DibujarLucesToolStripMenuItem
        '
        Me.DibujarLucesToolStripMenuItem.Name = "DibujarLucesToolStripMenuItem"
        Me.DibujarLucesToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.DibujarLucesToolStripMenuItem.Text = "Dibujar luces"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Label1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 283)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(400, 22)
        Me.StatusStrip1.TabIndex = 2
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'Label1
        '
        Me.Label1.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.Label1.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenOuter
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(126, 17)
        Me.Label1.Text = "Poliedro seleccionado: 0"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(265, 6)
        '
        'TeselacionToolStripMenuItem
        '
        Me.TeselacionToolStripMenuItem.Name = "TeselacionToolStripMenuItem"
        Me.TeselacionToolStripMenuItem.Size = New System.Drawing.Size(268, 22)
        Me.TeselacionToolStripMenuItem.Text = "Teselación"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(400, 305)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.Pic)
        Me.Controls.Add(Me.MenuStrip1)
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.Pic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Pic As System.Windows.Forms.PictureBox
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ArchivoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReiniciarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PoliedrosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AñadirPoliedroToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CuboToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PiramideToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents QuitarPoliedroToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FalsoConToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AnilloToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FocosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AñadirFocoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents QuitarFocoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SeleccionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SeleccionarPoliedroToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MovimientoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PararReanudarMovimientoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RotarPoliedroToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EscalarPoliedroToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents VisualizacionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SombrearToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IcosaedroToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DodecaedroToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents Label1 As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents CambiarColorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ColorDialog As System.Windows.Forms.ColorDialog
    Friend WithEvents TrasladarToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ModoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TrasladaraToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RepresentacionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OrtogonalToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IsometricaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConicaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OrtogonalGeneralToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PosiciónDeLaCámaraToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents XOYToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ZOXToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ZOYToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DibujarEjesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DistanciaDelPlanoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CalculoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MoverFocosConElCursorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EstablecerPosiciónDeUnFocoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RotarAlrededorToolStripMenuItem2 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EscalarFijoToolStripMenuItem3 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OrtogonalLibreToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DireccionCamaraToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CilindroToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ActivadoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConstantesDeDistanciaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExponenteDeReflexiónEspecularToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ComponentesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LuzAmbientalToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReflexiónDifusaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReflexiónEspecularToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents IntensidadesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LuzAmbientalToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReflexiónDifusaToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReflexiónEspecularToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CambiarColorFocoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConfiguracionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents EsferaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DatosToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FlatShadingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GouraudShadingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents WireFrameShadingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DepuracionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MostrarNormalesDeLasCarasToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MostrarNormalesDeLosVértiToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MostrarDatosDeLosVérticesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MostrarTrianToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MostrarCarabufferSeñaladaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SalidaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BufferToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DibujoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SombreadoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TodoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MostrarIndicesBufferToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents DibujarLucesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReestablecerTransformacionesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CambiarTipoFocoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PropiedadesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DirecciónToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeltaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RadioToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator12 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents TeselacionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
