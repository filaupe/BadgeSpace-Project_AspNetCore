import requests
 
def Get(link):
    print(requests.get(url=link).json())

def Post(link, dados):
    print(requests.post(url=link, data=dados).json())
    
def Patch(link, dados):
    print(requests.patch(url=link, data=dados).json())

def Put(link, dados):
    print(requests.put(url=link, data=dados).json())
    
def Delete(link, dados):
    print(requests.delete(url=link, data=dados).json())
    
#https://api.openweathermap.org/data/2.5/weather?q=Estância&appid=226558d8acfe0c04335cb9b2ce6393cf&lang=pt_br