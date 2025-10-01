# 📈 Stock Quote Alert

Uma aplicação de console em **C#** para monitorar em tempo real a cotação de ativos da **B3** (intervalos de 30s), enviando alertas por **e-mail** quando os preços ultrapassarem os limites definidos e salvando os valores em um arquivo **.csv**.

## 🚀 Funcionalidade

Este sistema monitora a cotação de um ativo e envia alertas via e-mail quando:

- A cotação **sobe acima** do valor de referência de **venda**.
- A cotação **cai abaixo** do valor de referência de **compra**.

### Exemplo de uso:

```bash
dotnet build
dotnet run -- PETR4 22.67 22.59
```
Com o executável compilado pode-se usar:
```bash
stock-quote-alert.exe PETR4 22.67 22.59
```

### 🛠️ Configurações de variáveis

Para que o envio de e-mails funcione corretamente, é necessário configurar o arquivo **config.json**, que contém os dados do remetente, do destinatário e do servidor SMTP.

✏️ O que você precisa ajustar:

- EmailTo: O e-mail que vai receber os alertas.

- SMTP.User: O e-mail remetente, ou seja, o que irá enviar as mensagens.

- SMTP.Key: A senha do e-mail remetente.

### 🛑 Limitações

Por usar a versão de teste gratuita da API [BRAPI](https://brapi.dev), o programa cota apenas valores dos ativos:
PETR4 (Petrobras) • MGLU3 (Magazine Luiza) • VALE3 (Vale) • ITUB4 (Itaú)
