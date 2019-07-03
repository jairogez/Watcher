# Watcher
Comando para executar: dotnet Watcher.dll <nome_do_arquivo>
Onde <nome_do_arquivo> é o caminho completo para o arquivo a ser monitorado. 
Necessária instalação do dotnet, disponível em https://dotnet.microsoft.com/download.
O app monitora os logs que vão sendo escritos em tempo de execução e, quando um novo login é efetuado de fora do Brasil, grava um log em <nome_do_arquivo>.log. 
A cada evento de escrita no arquivo que está sendo monitorado, o app guarda o índice atual de leitura num arquivo <nome_do_arquivo>.ini a fim de evitar que logins que ocorreram entre um eventual espaço de tempo em que o app esteve fechado não sejam monitorados. 
Caso <nome_do_arquivo> não seja informado, o arquivo a ser monitorado será ./log.txt.
