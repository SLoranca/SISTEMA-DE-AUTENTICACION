namespace SAAUR.MODELS.Entities.TemplatesEmail
{
    public class Auth2FactorTemplateEmail : ModelEmail
    { 
        public Auth2FactorTemplateEmail(string email, string code) {
            subject = "SAAUR- INICIO DE SESIÓN - 2FA";
            destination_email = email;
            body = "<!DOCTYPE html>" +
                            "<html lang='en'>" +
                            "<head>" +
                                "<meta charset='UTF-8'>" +
                                "<meta name='viewport' content='width=device-width, initial-scale=1.0'>" +
                                "<title>Confirmación de inicio de sesión</title>" +
                                "<style>" +
                                    "body {" +
                                        "font-family: Arial, sans-serif;" +
                                        "background-color: #f4f4f4;" +
                                        "margin: 0;" +
                                        "padding: 0;" +
                                    "}" +
                                    ".container {" +
                                        "max-width: 600px;" +
                                        "margin: 20px auto;" +
                                        "background-color: #fff;" +
                                        "padding: 20px;" +
                                        "border-radius: 5px;" +
                                        "box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);" +
                                    "}" +
                                    "h1 {" +
                                        "color: #333;" +
                                        "text-align: center;" +
                                    "}" +
                                    "p {" +
                                        "color: #555;" +
                                        "line-height: 1.6;" +
                                    "}" +
                                    ".btn {" +
                                        "display: inline-block;" +
                                        "background-color: #007bff;" +
                                        "color: #fff;" +
                                        "text-decoration: none;" +
                                        "padding: 10px 20px;" +
                                        "border-radius: 3px;" +
                                    "}" +
                                    ".btn:hover {" +
                                        "background-color: #0056b3;" +
                                    "}" +
                                "</style>" +
                            "</head>" +
                            "<body>" +
                                "<div class='container'>" +
                                    "<h1>SAAUR</h1>" +
                                    "<h3 style='text-align: center'>Confirmación de inicio de sesión</h3>" +
                                    "<p>Estimado Usuario,</p>" +
                                    "<p style='text-align: justify'>Has iniciado sesión en nuestro sistema con éxito. Si no reconoces esta actividad, por favor contacta con nuestro equipo de soporte.</p>" +
                                    "<p style='text-align: justify'>" +
                                        "Si no reconoces este inicio de sesión o si tienes alguna pregunta sobre la seguridad de tu cuenta, por favor, contáctanos de inmediato para que podamos ayudarte a tomar las medidas necesarias." +
                                    "</p>" +
                                    "<p style='text-align: center'>Codigo de Acceso<br>" + code + "</p>" +
                                    "<p>Atentamente,<br>El equipo de [Nombre de la empresa]</p>" +
                                "</div>" +
                            "</body>" +
                            "</html>";
        }
    }
}
