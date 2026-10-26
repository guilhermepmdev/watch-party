# WatchParty 

<p align="center">
  <img src="https://img.shields.io/badge/status-em%20desenvolvimento-blue" alt="Status">
  <img src="https://img.shields.io/badge/license-MIT-green" alt="License">
</p>

Uma aplicação web leve e descentralizada baseada em **WebRTC (PeerJS)** para transmissão de tela (Watch Party) em alta qualidade e chamadas de voz integradas entre amigos, sem a necessidade de servidores centrais pesados de mídia.

---

## 🚀 Funcionalidades

* **Transmissão de Tela P2P:** Compartilhe sua tela (com suporte a 1080p/720p a 30/60 FPS), áudio do sistema e otimizações para jogos, vídeos ou textos.
* **Chamadas de Voz Integradas:** Converse com seus amigos em tempo real durante a transmissão, com supressão de ruído ajustável.
* **Sistema de Contatos e Presença:** Adicione amigos facilmente por códigos de chamada únicos e verifique o status online/offline.
* **Baixa Latência:** Configurações ajustáveis de buffer e bitrate para equilibrar estabilidade e desempenho da rede.
* **Leve e Prático:** Interface moderna e minimalista construída com HTML5, CSS e JavaScript puro, empacotável em um executável leve (`.exe` com C# launcher).

---

## 🛠️ Tecnologias Utilizadas

* **Frontend:** HTML5, CSS3, JavaScript (Vanilla)
* **Comunicação P2P / WebRTC:** [PeerJS](https://peerjs.com/)
* **Launcher (Opcional):** C# (.NET) para empacotamento e execução local segura.

---

## 📦 Como Usar

### Como Espectador / Participante
1. Abra a aplicação no navegador ou execute o aplicativo local.
2. Insira o **Código de Transmissão** de 6 dígitos fornecido pelo seu amigo para assistir.
3. Utilize o **Código de Chamada** para se conectar por voz na aba de amigos.

### Como Transmissor (Host)
1. Clique em **"Transmitir tela"** e selecione a janela ou aba desejada.
2. Copie o **código gerado** e envie para os seus amigos.
3. Monitore a taxa de upload, bitrate e amigos conectados diretamente na interface.

---

## ⚙️ Configuração para Desenvolvimento

Se você quiser clonar e rodar o projeto localmente:

1. Clone o repositório:
   ```bash
   git clone [https://github.com/seu-usuario/watch-party.git](https://github.com/seu-usuario/watch-party.git)
