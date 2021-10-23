## Instruções

1. Realize o clone do projeto.
2. Configure a string de conexão. Você precisará configurar o MySql e rodar a Migration do banco.
3. Configure a porta de execução do projeto, verifique se está tudo correto.



## Endpoints

* Criar um novo servidor: **/api/servers**
* Remover um servidor existente: **/api/servers/{serverId} **
* Recuperar um servidor existente: **/api/servers/{serverId} **
* Checar disponibilidade de um servidor:  **/api/servers/available/{serverId} **
* Listar todos os servidores: **/api/servers **
* Adicionar um novo vídeo à um servidor: **/api/servers/{serverId}/videos **
* Remover um vídeo existente: **/api/servers/{serverId}/videos/{videoId}**
* Recuperar dados cadastrais de um vídeo: **/api/servers/{serverId}/videos/{videoId}**
* Download do conteúdo binário de um vídeo: **/api/servers/{serverId}/videos/{videoId}/binary **
* Listar todos os vídeos de um servidor: **/api/servers/{serverId}/videos **
* Reciclar vídeos antigos: **/api/recycler/process/{days}**
* Verificar status da reciclagem: **/api/recycler/status**

