# Unity_ManyGames

유니티로 만들어본 여러가지 게임들을 하나로 통합.

회원가입, 로그인 후 각 게임들 플레이 가능.

각 게임별 유저의 점수 저장, 랭킹 보드 기능.

<br> <br>

### DB 

https://mbaas.nifcloud.com/about.htm

db 는 NCMB mbaas (mobile backend as service) 서비스를 사용했습니다.

NCMB 에서 제공하는 application key 와 Client key 로 유니티와 ncmb 를 연결해서 데이터를 보관할수 있습니다.

<img src="https://github.com/LSH3333/Unity_ManyGames/assets/62237852/4e7bb8b2-3b8b-42f5-b101-edd82fa6ed7d.png" width="60%" height="60%"/>


https://github.com/LSH3333/Unity_ManyGames/blob/c79bf3f0f0e651308ca3da77280a224a1aaad34c/Assets/PublicResources/RankingSystem/Result.cs#L78-L95

이런 baas 서비스를 이용하면 간단하게 이런 식으로 클라이언트의 데이터를 서버로 보내 저장 할 수 있습니다. 

<br> <br>

### 여러가지 미니 게임들 

<img src="https://github.com/LSH3333/Unity_ManyGames/assets/62237852/3a569069-fcb8-446d-9c71-e428129d3aff.png" width="60%" height="60%"/>

<br>

기존에 만들어 놓은 미니게임들을 통합하면서 가장 신경 썼던 부분은 게임 시작 로직, 점수 저장, 결과창 등 모든 미니게임들에서 공통으로 쓰이는 부분을 추상적으로 구현해 각각의 게임에서 사용할수 있도록 하는 것이었습니다.

Manage 클래스에 공통으로 쓰이는 Canvas에 UI 띄우는 기능, 점수 등을 정의해놓고 각 미니게임들에서 상속받아 사용합니다.

예를들어 각 게임에는 점수들이 존재하기 때문에 무조건 Manage 에 저장하도록 하고, 

SetStart() 같은 게임 시작 로직은 모든 게임에 필요하지만 각 게임마다 로직이 살짝 다르기 때문에 abstract 로 만들었습니다. 

<br>

Manager 클래스 : 
https://github.com/LSH3333/Unity_ManyGames/blob/master/Assets/PublicResources/Manage/Manage.cs

Manager 클래스를 상속받아 사용하는 JumperManagerGame 클래스 : 
https://github.com/LSH3333/Unity_ManyGames/blob/master/Assets/Jumper/Scripts/JumperManagerGame.cs


<br> <br>


### 가입 

<img src="https://github.com/LSH3333/Unity_ManyGames/assets/62237852/1ed6c533-9d60-43cc-975e-dc40ceb9956c.png" width="60%" height="60%"/>


### 로그인 

<img src="https://github.com/LSH3333/Unity_ManyGames/assets/62237852/820d0dec-98be-40a9-b9ea-b23615b54f0f.png" width="60%" height="60%"/>


### 미니게임 목록 

<img src="https://github.com/LSH3333/Unity_ManyGames/assets/62237852/cc91c13f-ee98-496f-bd1c-1b0f4d4bfa12.png" width="60%" height="60%"/>

### 게임 별 랭킹 보드 열람 

<img src="https://github.com/LSH3333/Unity_ManyGames/assets/62237852/8223e876-ea40-4dea-a1d8-f8eb15c0450c.png" width="60%" height="60%"/>

<br>

### 게임 : FlappyBird

<details>
<summary>접기/펼치기</summary>

<img  alt="image" src="https://github.com/LSH3333/Unity_ManyGames/assets/62237852/071dae83-e7da-4aa8-b559-9b704f43c6a0" width="60%" height="60%">

FlappyBird 게임 구현의 중점은 중 Rigidbody 와 Collider 의 사용이었습니다.

Rigidbody 를 통해 오브젝트에 적용되는 모든 '힘' 을 관리할수 있습니다. 

Collider 는 오브젝트와 다른 오브젝트의 접촉, 충돌을 관리 합니다.

Rigidbody 를 통해 Bird 의 움직임을, Collider 를 통해 Bird 와 장애물의 충돌을 관리할수 있습니다.

  
</details>


<br>

### 게임 : AngryBird 

<details>
<summary>접기/펼치기</summary>

AngryBird 게임 구현의 중점은 랜덤 구조물 생성이었습니다.

랜덤으로 구조물 소환시 문제점은 유니티에서 오브젝트가 강체(Rigidbody) 를 갖는 경우 오브젝트들이 게임에 소환되는 순간 서로 간섭을 받아 무너져 내릴수 있습니다.

따라서 오브젝트의 크기에 따라 정확한 소환 좌표들을 계산해서, 정확한 위치에 소환해야 합니다. 

<img src="https://github.com/LSH3333/Unity_ManyGames/assets/62237852/5b3e28ab-7b19-4627-85ee-2f8c59a8e04a.png" width="60%" height="60%"/>

<img src="https://github.com/LSH3333/Unity_ManyGames/assets/62237852/82db32c4-ced8-4c08-bb39-561d8ac61ef7.png" width="60%" height="60%"/>


https://github.com/LSH3333/Unity_ManyGames/blob/master/Assets/AngryBird/Scripts/AB_CreateMap.cs#L1-L64

https://github.com/LSH3333/Unity_ManyGames/blob/3bfa5d440acc4491a8f7ab5eeaa0ca4ca79ce737/Assets/AngryBird/Scripts/AB_CreateMap.cs#L1-L64
  
</details>


