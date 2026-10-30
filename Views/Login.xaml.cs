namespace ocutiExamen.Views;

public partial class Login : ContentPage
{
        string[,] usuarios = new string[,]
    {
        { "recepcion", "gym2026" },
        { "entrenador", "fit_01" },
        { "uisrael", "2026" },
        { "gerente", "admin123" }
    };

    public Login()
    {
        InitializeComponent();
    }

    private async void btnIniciarSesion_Clicked(object sender, EventArgs e)
    {
        try
        {
            string usuarioingresado = txtUsuario.Text?.ToLower().Trim(); 
            string contrasenaIngresada = txtcontrasena.Text; 

            bool accesoConcedido = false;

            
            for (int i = 0; i < 4; i++)
            {
                if (usuarios[i, 0] == usuarioingresado && usuarios[i, 1] == contrasenaIngresada)
                {
                    accesoConcedido = true;
                    break;
                }
            }

            if (accesoConcedido)
            {
                
                await Navigation.PushAsync(new Registro(usuarioingresado));
            }
            else
            {
                await DisplayAlertAsync("ERROR", "Usuario o Contraseña incorrectos", "Cerrar");
                txtUsuario.Text = "";
                txtcontrasena.Text = "";
            }
        }
        catch (Exception ex)
        {
            
            await DisplayAlertAsync("ERROR", ex.Message, "Cerrar");
        }
    }

    private async void btnAcercaDe_Clicked(object sender, EventArgs e)
    {
        
        await DisplayAlertAsync("Acerca de", "Estudiante: Tu Nombre", "OK");
    }
}