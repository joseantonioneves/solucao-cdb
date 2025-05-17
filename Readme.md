# solucao-cdb

Exercício de teste para a B3

## Manual de Instalação e Execução

Este projeto utiliza Docker para facilitar a execução do backend (API ASP.NET Core) e do frontend (Angular).  
Siga os passos abaixo para rodar a solução completa em sua máquina.

---

### **Pré-requisitos**

- Docker version 28.0.1, build 068a01e instalado
- [Docker Desktop](https://www.docker.com/products/docker-desktop) instalado e em execução (ajuda na avaliação)
- Certifique-se que a versão do node instalada não seja acima da versão 20.19.12. Versões acima são **incompatíveis** com o Angular17.
- [Git](https://git-scm.com/) instalado (opcional, para clonar o repositório)
- Certifique-se de que as portas **8080**, **8443** e **4200** estão livres
---

### **Passos para execução**

1. **Clone o repositório (se necessário):**
   ```sh
   git clone <url-do-repositorio>
   cd solucao-cdb
   ```

2. **Execute o script de deploy:**
   - No Windows, clique duas vezes no arquivo `run_all_docker.bat`  
     **ou** execute pelo terminal:
     ```sh
     run_all_docker.bat
     ```

   O script irá:
   - Parar e remover containers antigos do backend e frontend
   - Recriar as imagens Docker do backend e frontend
   - Subir os containers necessários, mapeando as portas corretamente

3. **Acesse a aplicação:**
   - **Frontend Angular:**  
     [http://localhost:4200](http://localhost:4200)
   - **Backend API (Swagger):**  
     [https://localhost:8443/swagger](https://localhost:8443/swagger)

---

### **Observações**

- O backend utiliza um certificado PFX para HTTPS, já incluído no projeto.
- O frontend se comunica com o backend via HTTPS na porta 8443.
- Se for necessário liberar CORS para outros domínios, ajuste a configuração no arquivo `Program.cs` do backend.
- Para parar os containers, utilize o Docker Desktop ou rode:
  ```sh
  docker stop solucao-cdb-api-container solucao-cdb-frontend-container
  ```

---

### **Dúvidas ou problemas**

- Certifique-se de que o Docker está rodando normalmente.
- Verifique se as portas não estão em uso por outros serviços.
- Consulte os logs dos containers pelo Docker Desktop para diagnóstico de erros.
  
---

