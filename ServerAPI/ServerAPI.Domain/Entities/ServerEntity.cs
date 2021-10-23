using System.Net.NetworkInformation;

namespace ServerAPI.Domain.Entities
{
    public class ServerEntity : BaseEntity
    {
        public string Nome { get; private set; }
        public string IP { get; private set; }
        public int Porta { get; private set; }

        public ServerEntity() { }

        public ServerEntity(string nome, string ip, int porta)
        {
            Nome = nome;
            IP = ip;
            Porta = porta;
        }

        public bool IsDisponivel()
        {
            var ping = new Ping();

            string url;

            if (Porta == 0)
                url = $"{IP}";
            else
                url = $"{IP}:{Porta}";

            var result = ping.SendPingAsync(url).GetAwaiter().GetResult();

            return result.Status == IPStatus.Success;
        }
    }
}
