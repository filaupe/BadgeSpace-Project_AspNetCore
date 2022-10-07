from Requests import Get, Post, Put, Patch, Delete

if __name__ == "__main__":
    while True:
        match input("Digite o tipo requisição: ").lower():
            case "get":
                Get(link=input("Insira o link: "))
            case "post":
                Post(link=input("Insira o link: "))
            case "put":
                Put(link=input("Insira o link: "))
            case "patch":
                Patch(link=input("Insira o link: "))
            case "delete":
                Delete(link=input("Insira o link: "))