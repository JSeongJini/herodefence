# HeroDefence(2023)
[herodefence.unitypackage](https://drive.google.com/file/d/1ZZG8nz8r75AW-PWRRvxoH5fk0SydrCO4/view?usp=share_link)

[herodefence.apk](https://drive.google.com/file/d/183ud-WOYmAfYZcV_d_lEXhIj9n2IIb_n/view?usp=share_link)

   
### 프로젝트 소개
- HeroDefence는 포트폴리오겸 개인 역량을 위해 공부하며 만든 프로젝트입니다.
- 게임 최적화와 디자인패턴 사용에 중점을 두고 제작하였습니다.
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
#### 문자열로 UI요소를 식별하는 시스템   
유니티 UGUI로 UI 작업을 하다보면, UI 컴포넌트들을 제어하기위해 상위 객체에서 참조를 가져야 하는 경우가 많다. 이 때, 인스펙터창에서 드래그앤드롭으로 참조를 연결해주게 되는데 개발자의 실수로 참조를 연결해주지 않거나, UI를 수정하다보면 이 참조가 깨지는 경우가 빈번하다. 이 문제를 피하기 위해서 path라 불리는 문자열로 UI 컴포넌트들을 식별하는 방법을 고안해보았다.

![uipath](https://user-images.githubusercontent.com/70570420/232442006-39890dd9-a561-4a9e-a994-7278d8d52ab1.PNG)
UI 컴포넌트에 UI Path 컴포넌트를 달아주고, Path를 입력하면
```C#
private static IDictionary<string, UIPath> views = new Dictionary<string, UIPath>();
```

문자열을 키값으로 하는 딕셔너리에 등록되고 프로젝트 전역에서 문자열만으로 해당 UI를 참조할 수 있다

```C#
UIContext.GetUIByPath("BossAlert", (result) => {
                BossAlert alert = result as BossAlert;
                alert.Show();
            });
```

- 장점   
 UI 작업 중 참조를 연결해주는 번거로움이 줄어든다.   
 UI 요소의 Path값만 상호협의되면, UI 디자인과 스크립트 작업이 별개로 이루어질 수 있다.
 <br/>


#### 구글 스프레드시트를 데이터베이스로 활용





