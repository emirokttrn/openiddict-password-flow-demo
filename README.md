ACIKLAMA: postman garip bi sekilde acces tokeni kopyalaip refresh tokene erisilmiyor cunku kopyaliyamadigi kisimlar var yuksek ihtimalle veya o kadar uzunlugu kabul etmiyor sebebini cozemedim ama baska cozum var
-------------------------------------

1. bir terminalde dotnet run calistirip projeyi ayaga kaldirin 

-------------------------------------

2.baska bi terminalde yazilan adimlari step by step yapin

'''''''''''''''''''''''''''''''''''''

2.1 nano test.sh

'''''''''''''''''''''''''''''''''''''

2.2  #!/bin/bash
echo "--- İstek atılıyor ---"
RESPONSE=$(curl -s -X POST http://localhost:5127/connect/token -d "grant_type=password&username=emir&password=1234&client_id=test-client&client_secret=test-secret&scope=offline_access")
echo "--- Cevap ---"
echo "$RESPONSE"
echo "--- Cevap sonu ---"

'''''''''''''''''''''''''''''''''''''

2.3 sirayla Ctrl+O, Enter, Ctrl+X

-------------------------------------

3. bash test.sh