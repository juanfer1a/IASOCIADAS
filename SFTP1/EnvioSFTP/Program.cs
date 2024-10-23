using System;
using Renci.SshNet;

namespace EnvioSFTP
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Application.Run(new Form1());

            {
                // Configuración de la conexión SFTP
                string host = "sftpgo.alephcrm.com";
                string username = "impcelestesftp23513";
                string password = "celeste#23513";
                int port = 2022; // Puerto por defecto para SFTP

                // Ruta local del archivo que deseas enviar
                string localFilePath = @"C:\Celeste.csv";

                // Ruta remota donde deseas guardar el archivo en el servidor SFTP
                string remoteDirectory = "/A procesar";

                // Establecer conexión SFTP
                using (var client = new SftpClient(host, port, username, password))
                {
                    try
                    {
                        client.Connect();

                        if (client.IsConnected)
                        {
                            // Subir archivo al servidor SFTP
                            using (var fileStream = System.IO.File.OpenRead(localFilePath))
                            {
                                client.UploadFile(fileStream, remoteDirectory + "/" + System.IO.Path.GetFileName(localFilePath));
                            }

                            Console.WriteLine("Archivo enviado exitosamente.");
                        }
                        else
                        {
                            Console.WriteLine("No se pudo conectar al servidor SFTP.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                    finally
                    {
                        client.Disconnect();
                    }
                }
            }

        }
    }
}
