using NuGet.Protocol;
using ProjectsMecsaSPA.Pages.Chat;
using System;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using TL; // Namespace de WTelegramClient

namespace ProjectsMecsaSPA.Services
{
    public class TelegramClientUser
    {
        private readonly WTelegram.Client client;

        public TelegramClientUser()
        {
            client = new WTelegram.Client(Config);

            CrearGrupoAsync("Grupo de prueba").Wait();

        }

        // Método para crear un grupo en Telegram
        public async Task<string> CrearGrupoAsync(string nombreGrupo)
        {
            using (client)
            {
                await client.LoginUserIfNeeded();

                // Crear grupo en Telegram (se requiere al menos un usuario además del creador)
                var x = await client.Messages_CreateChat(new InputUser[] { }, nombreGrupo);
                var updates = x.updates;
                // Verificar si updates.chats tiene elementos
                if (updates.Chats == null || !updates.Chats.Any())
                {
                    throw new Exception("Error: No se encontró el grupo creado en la respuesta.");
                }

                // Obtener el ID del grupo creado
                var chat = updates.Chats.Values.FirstOrDefault();
                if (chat == null)
                {
                    throw new Exception("Error: No se pudo extraer el chat del diccionario.");
                }

                // Convertir el ID del chat en un InputPeerChat
                var inputPeer = new InputPeerChat(chat.ID);

                // Generar un enlace de invitación usando el InputPeerChat
                var invite = await client.Messages_ExportChatInvite(inputPeer);
                Console.WriteLine($"🔗 Enlace de invitación: {invite.ToJson()}");

                return invite.ToString();
            }
        }

        // Configuración de la API de Telegram
        private static string Config(string what) => what switch
        {
            "api_id" => "",       // ⚠️ Reemplaza con tu API ID real
            "api_hash" => "", // ⚠️ Reemplaza con tu API Hash real
            "phone_number" => "",  // ⚠️ Reemplaza con tu número de Telegram
            _ => null
        };
    }
}
