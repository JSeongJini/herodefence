# HeroDefence(2023)
[프로젝트 압축 herodefence.zip](https://drive.google.com/file/d/1NrU-DGGyGGWRqD6lahZ5h21eFyx6w2GV/view?usp=share_link)   
[APK herodefence.apk](https://drive.google.com/file/d/183ud-WOYmAfYZcV_d_lEXhIj9n2IIb_n/view?usp=share_link)
   
   
### 프로젝트 소개
- HeroDefence는 포트폴리오겸 개인 역량을 위해 공부하며 만든 프로젝트입니다.
- Unity 2021.3.15f1 버전으로 제작되었으며 개발 기간은 약 3개월입니다.
- 유니티를 통해 전반적인 게임을 제작할 수 있다는 역량을 보여드리기 위해 제작하였습니다.
<br/>
   
### 게임 소개
- HeroDefence는 클리커 디펜스 게임으로, 주인공의 무기를 강화시켜 몰려오는 몬스터 웨이브를 막아내는 게임입니다.
- 주인공의 무기는 상점에서 강화석을 구매하여 특정 스탯을 강화할 수 있으며, 매 라운드마다 주어지는 선택지에서 무기 버프를 획득할 수 있습니다.
- 몬스터가 50개체 이상이 되면 디펜스에 실패합니다.
- 매 5라운드마다 등장하는 보스 몬스터를 시간안에 처치하지 못하면 디펜스에 실패합니다.
- 마지막 라운드(40)가 지나면 디펜스에 성공하며, 점수를 환산하여 랭킹에 등록됩니다.
<br/>
   
### 게임 플레이
![herodefence_gameplay](https://user-images.githubusercontent.com/70570420/232434951-175965f0-65d2-4fdd-8a8b-3d70e0309801.png)
<br/><br/><br/>


### 시도한 것
#### 다양한 해상도에 적응하기 위한 카메라와 캔버스 조정
모바일 기기의 해상도는 다양한데, 해상도 문제로 인해 플레이에 지장을 끼칠 수 있다. 어떤 해상도에서 플레이 되더라도, 게임에 지장이 없도록 직교카메라의 사이즈를 조절하여 보여주어야 하는 최소한의 영역을 보여주도록 보장하고, 해상도의 비율에 따라서 캔버스 스케일러의 Match값을 조정해주었다.

![resolution](https://user-images.githubusercontent.com/70570420/232454182-204cc5d8-93aa-4123-b52a-2aef54098ad7.png)
```C#
Camera cam = Camera.main;

float sizeToTargetWidth = targetWidth / (2f * cam.aspect);
float sizeToTargetHeight = targetHeight / 2f;

float size = (sizeToTargetWidth >= sizeToTargetHeight) ? sizeToTargetWidth : sizeToTargetHeight;

cam.orthographicSize = size;
```

```C#
if (!Camera.main) return;

if(Camera.main.aspect > (9f / 16f))
{
   scaler.matchWidthOrHeight = 1f;
}
else
{
   scaler.matchWidthOrHeight = 0f;
}
```
<br/>

#### 문자열로 UI요소를 식별하는 시스템   
유니티 UGUI로 UI 작업을 하다보면, UI 컴포넌트들을 제어하기위해 상위 객체에서 참조를 가져야 하는 경우가 많다. 이 때, 인스펙터창에서 드래그앤드롭으로 참조를 연결해주게 되는데 개발자의 실수로 참조를 연결해주지 않거나, UI를 수정하다보면 이 참조가 깨지는 경우가 빈번하다. 이 문제를 피하기 위해서 path라 불리는 문자열로 UI 컴포넌트들을 식별하는 방법을 고안해보았다.

![uipath](https://user-images.githubusercontent.com/70570420/232442006-39890dd9-a561-4a9e-a994-7278d8d52ab1.PNG)
UI 컴포넌트에 UI Path 컴포넌트를 달아주고, Path를 입력하면
```C#
private static IDictionary<string, UIPath> views = new Dictionary<string, UIPath>();
```

문자열을 키값으로 하는 딕셔너리에 등록되고 프로젝트 전역에서 문자열만으로 해당 UI를 참조할 수 있다.

```C#
UIContext.GetUIByPath("BossAlert", (result) => {
    BossAlert alert = result as BossAlert;
    alert.Show();
});
```

- 장점   
 UI 작업 중 참조를 연결해주는 번거로움이 줄어든다.   
 UI 요소의 Path값만 상호협의되면, UI 디자인과 스크립트 작업이 별개로 이루어질 수 있다.
 Addressable Asset의 address와 path를 통일 시켜 UI를 관리할 수 있다.
 <br/>


#### 구글 스프레드시트를 데이터베이스로 활용
게임의 백엔드는 다양한 방법으로 구현할 수 있지만, 구글 스프레드 시트와 Apps Script를 활용하여 데이터베이스를 구축할 수 있는 방법을 알게되어 시도해 보았다.

![googlesheet](https://user-images.githubusercontent.com/70570420/232447821-db44345b-6834-4735-9005-b3d00e613682.png)

- 장점   
 과금 걱정없는 무료이다.   
 컴퓨터를 켜놓지 않아도 된다.  
 게임이 아니더라도, 여러 곳에서 사용할 수 있을 것 같다.
 <br/>




