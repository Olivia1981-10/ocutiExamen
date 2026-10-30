namespace ocutiExamen.Views;

public partial class Resumen : ContentPage
{
    public Resumen(string usuario, string nombre, string apellido, string edad, string fecha, string plan, bool casillero, double montoInicial, double pagoMensual, double pagoTotal)
    {
        InitializeComponent();
        try
        {
            lblUsuarioConectado.Text = $"Usuario conectado: {usuario}";
            lblNombre.Text = nombre;
            lblApellido.Text = apellido;
            lblEdad.Text = edad;
            lblFecha.Text = fecha;
            lblPlan.Text = plan;
            lblCasillero.Text = casillero ? "Sí" : "No";
            lblMonto.Text = montoInicial.ToString("F2");
            lblPagoMensual.Text = pagoMensual.ToString("F2");
            lblPagoTotal.Text = pagoTotal.ToString("F2");
        }
        catch (Exception ex)
        {
            DisplayAlert("ERROR", ex.Message, "Cerrar");
        }
    }

    private async void btnNuevo_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void btnCerrar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
}