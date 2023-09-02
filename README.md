# Unity_ManyGames

유니티로 만들어본 여러가지 게임들을 하나로 통합.

회원가입, 로그인 후 각 게임들 플레이 가능.

각 게임별 유저의 점수 저장, 랭킹 보드 기능.

<br> <br>

### 가입 

<img src="https://github.com/LSH3333/Unity_ManyGames/assets/62237852/1ed6c533-9d60-43cc-975e-dc40ceb9956c.png" width="60%" height="60%"/>


### 로그인 

<img src="https://github.com/LSH3333/Unity_ManyGames/assets/62237852/820d0dec-98be-40a9-b9ea-b23615b54f0f.png" width="60%" height="60%"/>


### 미니게임 목록 

<img src="https://github.com/LSH3333/Unity_ManyGames/assets/62237852/cc91c13f-ee98-496f-bd1c-1b0f4d4bfa12.png" width="60%" height="60%"/>

### 게임 별 랭킹 보드 열람 

<img src="https://github.com/LSH3333/Unity_ManyGames/assets/62237852/8223e876-ea40-4dea-a1d8-f8eb15c0450c.png" width="60%" height="60%"/>


### 게임 : AngryBird 

<details>
<summary>접기/펼치기</summary>

앵그리버드 게임 구현의 중점은 랜덤 구조물 생성이었습니다.

랜덤으로 구조물 소환시 문제점은 유니티에서 오브젝트가 강체(Rigidbody) 를 갖는 경우 오브젝트들이 게임에 소환되는 순간 서로 간섭을 받아 무너져 내릴수 있습니다.

따라서 오브젝트의 크기에 따라 정확한 소환 좌표들을 계산해서, 정확한 위치에 소환해야 합니다. 

<img src="https://github.com/LSH3333/Unity_ManyGames/assets/62237852/5b3e28ab-7b19-4627-85ee-2f8c59a8e04a.png" width="60%" height="60%"/>

<img src="https://github.com/LSH3333/Unity_ManyGames/assets/62237852/82db32c4-ced8-4c08-bb39-561d8ac61ef7.png" width="60%" height="60%"/>


https://github.com/LSH3333/Unity_ManyGames/blob/master/Assets/AngryBird/Scripts/AB_CreateMap.cs#L1-L64

https://github.com/LSH3333/Unity_ManyGames/blob/3bfa5d440acc4491a8f7ab5eeaa0ca4ca79ce737/Assets/AngryBird/Scripts/AB_CreateMap.cs#L1-L64
  
</details>


