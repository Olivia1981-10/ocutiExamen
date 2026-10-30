using System.Globalization; // Necesario para la cultura de la fecha

namespace ocutiExamen.Views;

public partial class Registro : ContentPage
{
    string usuarioActual;
    double pagoMensualCalculado = 0;

    public Registro(string usuario)
    {
        InitializeComponent();
        usuarioActual = usuario;
        lblUsuarioConectado.Text = $"Usuario conectado: {usuario}";

        // No permitir fechas pasadas
        dpFecha.MinimumDate = DateTime.Today;
    }

    private async void btnCalcular_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Validar Edad
            if (!int.TryParse(txtEdad.Text, out int edad) || edad < 16)
            {
                await DisplayAlertAsync("Error", "La edad debe ser mayor o igual a 16", "OK");
                return;
            }

            // Validar Monto Inicial
            if (!double.TryParse(txtMonto.Text, out double montoInicial) || montoInicial < 120 || montoInicial > 600)
            {
                await DisplayAlertAsync("Error", "El monto inicial debe estar entre 120 y 600", "OK");
                return;
            }

            // Fórmulas
            double costoTotal = 1200;
            double resto = costoTotal - montoInicial;
            double cuotaBase = resto / 5;
            double cuotaFinal = cuotaBase + (costoTotal * 0.02);

            if (swCasillero.IsToggled)
            {
                cuotaFinal += 10;
            }

            pagoMensualCalculado = cuotaFinal;
            txtPagoMensual.Text = pagoMensualCalculado.ToString("F2");
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("ERROR", ex.Message, "Cerrar");
        }
    }

    private async void btnVerResumen_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellido.Text) || pkPlan.SelectedItem == null)
            {
                await DisplayAlertAsync("Error", "Complete los datos y seleccione un plan.", "OK");
                return;
            }

            if (string.IsNullOrEmpty(txtPagoMensual.Text))
            {
                await DisplayAlertAsync("Error", "Debe calcular el pago primero.", "OK");
                return;
            }

            double montoInicial = double.Parse(txtMonto.Text);
            double pagoTotal = montoInicial + (pagoMensualCalculado * 5);

            
            DateTime fechaSeleccionada = dpFecha.Date ?? DateTime.Today;

            
            string fechaFormateada = $"{fechaSeleccionada.Day:D2}/{fechaSeleccionada.Month:D2}/{fechaSeleccionada.Year}";

            // Navegar a Resumen
            await Navigation.PushAsync(new Resumen(
                usuarioActual,
                txtNombre.Text,
                txtApellido.Text,
                txtEdad.Text,
                fechaFormateada, 
                pkPlan.SelectedItem.ToString(),
                swCasillero.IsToggled,
                montoInicial,
                pagoMensualCalculado,
                pagoTotal
            ));
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("ERROR", ex.Message, "Cerrar");
        }
    }
}