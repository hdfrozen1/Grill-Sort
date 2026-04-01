# UI Game Tutorial — Grill Sort

This document describes the layout, components, and Coplay setup instructions for each UI screen in `Assets/Resources/UIGame`.

---

## 1. Home Screen (`home.png`)

### Overview
The main lobby screen shown when the player opens the game. It features animated characters, the food truck, and all primary navigation elements.

### Components

#### Top Bar
| Element | Description |
|---|---|
| **Settings Button** | Square icon button (gear icon) in top-left corner |
| **Lives Display** | Heart icon + count text ("5 Full") — pill-shaped panel, top-center |
| **Coins Display** | Coin icon + count text ("150") — pill-shaped panel, top-right |

#### Left Side Buttons (Vertical Stack)
| Element | Description |
|---|---|
| **Star Chest Button** | Treasure chest icon button with progress bar below ("223/500") |
| **Gallery Button** | Book icon button with label "Gallery" |
| **Remove Ads Button** | TV+block icon button with label "Remove Ads" |

#### Center / Scene Area
| Element | Description |
|---|---|
| **Game Scene** | Animated food truck with characters — purely decorative background |
| **Shop Sign** | Empty wooden billboard positioned right of the truck |

#### Bottom
| Element | Description |
|---|---|
| **Play Button** | Large green rounded button with "Play" and "Level 6" labels |
| **Progress Bar Row** | Two horizontal bars indicating chapter/level progress |
| **Bottom Tab Bar** | Three tabs: **Shop** (left), **Home** (center, active), **Lock/Leaderboard** (right) |

### Coplay Setup Instructions

```
1. Create a Canvas (Screen Space – Overlay) named "HomeCanvas".

2. Top Bar:
   - Create a panel named "TopBar" anchored to the top of the canvas (stretch width, fixed height ~100px).
   - Add a Button child "BtnSettings" at top-left with a gear sprite.
   - Add a panel "LivesDisplay" (center-top): Image (pill background) + Image (heart icon) + Text "5 Full".
   - Add a panel "CoinsDisplay" (top-right): Image (pill background) + Image (coin icon) + Text "150".

3. Left Side Stack:
   - Create a VerticalLayoutGroup panel "LeftButtons" anchored to left-center.
   - Add Button "BtnStarChest": Image (chest icon) + child Slider "StarProgress" (value: 223/500).
   - Add Button "BtnGallery": Image (book icon) + Text "Gallery".
   - Add Button "BtnRemoveAds": Image (TV icon) + Text "Remove Ads".

4. Center Scene:
   - Add an Image "GameScene" filling the center area with the food truck illustration.
   - No interaction needed (non-clickable background).

5. Bottom Area:
   - Add Button "BtnPlay" anchored above tab bar: green rounded background + Text "Play" (large) + Text "Level 6" (small, below).
   - Add a panel "LevelProgress" with two child Sliders representing level/chapter progress.
   - Add a panel "TabBar" anchored to bottom (stretch width, fixed height ~120px):
     - Button "TabShop": Icon + Text "Shop"
     - Button "TabHome": Icon + Text "Home" (set as active/selected state)
     - Button "TabLeaderboard": Lock icon (or trophy icon)

6. Use a VerticalLayoutGroup + ContentSizeFitter on LeftButtons for automatic spacing.
7. Set Canvas Scaler: UI Scale Mode = Scale With Screen Size, Reference Resolution = 720x1560.
```

---

## 2. Setting Popup (`setting.png`)

### Overview
A modal popup dialog opened by the Settings button. Overlays the Home screen with a dimmed background.

### Components

| Element | Description |
|---|---|
| **Dim/Overlay** | Semi-transparent dark background covering full screen |
| **Popup Panel** | Rounded orange-bordered white panel, centered |
| **Title Banner** | Gray rounded tab at top of panel with text "Setting" |
| **Close Button** | Brown "X" button at top-right of panel |
| **Sound Row** | Speaker icon + "Sound" label + Toggle (green ON state) |
| **Vibration Row** | Vibration icon + "Vibration" label + Toggle (green ON state) |
| **Restore Purchase Button** | Yellow/orange rounded button with text "Restore Purchase" |
| **Version Label** | Small text "1.3.4" at bottom-right corner of panel |

### Coplay Setup Instructions

```
1. Create a full-screen panel "SettingOverlay" (anchored stretch-stretch) with a semi-transparent black Image component (alpha ~0.6). Set it as a child of the main Canvas.

2. Add a child Panel "SettingPopup":
   - Anchor: center
   - Size: ~600x900px
   - Image: white background with orange border (use a 9-slice sprite)

3. Add "TitleBanner" above the panel top edge:
   - Image: gray rounded rectangle (9-slice)
   - Text: "Setting"

4. Add Button "BtnClose" at top-right of SettingPopup:
   - Image: brown rounded square
   - Text: "X"

5. Add a panel "OptionsGroup" inside SettingPopup using VerticalLayoutGroup:
   - Row "SoundRow": HorizontalLayoutGroup
     - Image (speaker icon)
     - Text "Sound"
     - Toggle "ToggleSound": configure with green slider graphic
   - Row "VibrationRow": HorizontalLayoutGroup
     - Image (vibration icon)
     - Text "Vibration"
     - Toggle "ToggleVibration": configure with green slider graphic

6. Add Button "BtnRestorePurchase":
   - Image: yellow-orange rounded rectangle (9-slice)
   - Text: "Restore Purchase"

7. Add Text "VersionLabel": "1.3.4", small font, anchored to bottom-right.

8. BtnClose onClick → deactivate SettingOverlay GameObject.
9. Animate popup entrance using DOTween scale from 0 to 1 (punch or ease-out-back).
```

---

## 3. Remove Ads Popup (`removeads.png`)

### Overview
A modal popup offering the player an option to purchase the removal of ads, including a bundle deal with bonus items.

### Components

| Element | Description |
|---|---|
| **Dim/Overlay** | Semi-transparent background |
| **Popup Panel** | Rounded orange-bordered panel, centered |
| **Title Banner** | Gray rounded tab "Remove Ads" |
| **Close Button** | Brown "X" at top-right |
| **Hero Illustration** | Large ADS TV image surrounded by coins, stars, magnet, arrow items |
| **Benefits Strip** | Light-beige rounded panel containing 5 item icons with labels: No Ads, 2000 coins, x2 snowflake, x2 magnet, x2 arrows |
| **Purchase Button** | Large green rounded button with price "254.000 đ" |

### Coplay Setup Instructions

```
1. Create full-screen "RemoveAdsOverlay" panel (same overlay pattern as Setting).

2. Add child Panel "RemoveAdsPopup":
   - Size: ~600x1100px, centered
   - Image: white background with orange 9-slice border

3. Add "TitleBanner": gray rounded rectangle + Text "Remove Ads"

4. Add Button "BtnCloseAds" at top-right (same X button style as Setting popup).

5. Add Image "HeroIllustration": the ADS TV artwork, anchored in upper portion of the popup.

6. Add Panel "BenefitsStrip":
   - Image: beige/light-brown rounded rectangle (9-slice)
   - HorizontalLayoutGroup with 5 child columns, each with:
     - Image (benefit icon: No Ads / Coin / Snowflake / Magnet / Arrows)
     - Text (label: "No Ads" / "2000" / "x2" / "x2" / "x2")

7. Add Button "BtnPurchaseAds":
   - Image: green rounded rectangle
   - Text: "254.000 đ" (bold, white)
   - OnClick → trigger IAP purchase flow

8. BtnCloseAds onClick → deactivate RemoveAdsOverlay.
```

---

## 4. Shop Screen (`shop.png`)

### Overview
A full scrollable screen (Shop tab) showing purchasable item packs. Accessed via the Shop tab bar button.

### Components

#### Top Bar
| Element | Description |
|---|---|
| **Coins Display** | Coin icon + "150" — top-right, pill panel |
| **Striped Header** | Decorative red/white striped awning background image at top |

#### Shop Items (Scrollable List)
| Item | Contents | Price |
|---|---|---|
| **Remove Ads** | ADS TV icon | 204.000 đ |
| **No Ads Bundle** | No Ads + 2000 coins + x2 snowflake + x2 magnet + x2 arrows | 254.000 đ |
| **Starter Pack** | 3000 coins + x1 snowflake + x1 magnet + x1 arrows + x1 star + 1h power-ups + 1h hearts | 127.000 đ |
| **Small Pack** | 6000 coins + x4 power-ups + 2h timers | 254.000 đ |
| **Big Pack** (partially visible) | 10000 coins + x8 power-ups + 4h timers | 509.000 đ |

#### Bottom Navigation
| Element | Description |
|---|---|
| **Tab Bar** | Shop (active), Home, Lock tabs |

### Coplay Setup Instructions

```
1. Create Canvas "ShopCanvas" (or reuse main canvas with a "ShopPanel" child).

2. Top Area:
   - Image "ShopHeader": striped awning background, anchored to top, full width
   - Panel "CoinsDisplay" at top-right: coin icon + Text "150"

3. Create a ScrollView "ShopScrollView":
   - Anchor: stretch both axes below header, above tab bar
   - Scroll direction: Vertical only
   - Content panel uses VerticalLayoutGroup with spacing

4. Inside the ScrollView Content, add shop item panels:

   a. Panel "IAPRowTop" with HorizontalLayoutGroup:
      - Panel "ItemRemoveAds": Image (ADS icon) + Text "Remove Ads" + Button "204.000 đ" (green)
      - Panel "ItemNoAdsBundle": HorizontalLayoutGroup (5 icons) + Text "No Ads Bundle" + Button "254.000 đ" (green)

   b. Panel "ItemStarterPack" with a styled border:
      - Left side: Image (coin stack) + Text "3000"
      - Center: Grid of 4 item icons (snowflake x1, magnet x1, arrows x1, star x1)
      - Right section: Timer items (1h power-up timer, 1h chest, 1h stars, 1h hearts)
      - Bottom: Text "Starter Pack" + Button "127.000 đ" (green)

   c. Panel "ItemSmallPack" (same structure, x4 quantities, 2h timers, "254.000 đ")

   d. Panel "ItemBigPack" (same structure, x8 quantities, 4h timers, "509.000 đ")

5. For each pack panel, use a 9-slice rounded background sprite (beige inside, colored bottom strip for label area).

6. Tab Bar (same component as Home):
   - TabShop set to active/selected state
   - TabHome and TabLeaderboard as inactive

7. OnClick for each purchase button → trigger corresponding IAP product ID.
```

---

## 5. Star Chest Popup (`starchest.png`)

### Overview
A modal popup encouraging players to collect stars to unlock a reward chest. Displays current star progress.

### Components

| Element | Description |
|---|---|
| **Dim/Overlay** | Semi-transparent dark background |
| **Popup Panel** | Rounded orange-bordered panel, centered |
| **Title Banner** | Gray rounded tab "Star Chest" |
| **Close Button** | Brown "X" at top-right |
| **Progress Bar** | Star icon + green progress bar with "223/500" text + chest icon at the right end |
| **Reward Preview Area** | Beige/tan bordered panel containing: Description text, chest illustration, reward item icons (arrows, magnet, coin, star, snowflake) |
| **Description Text** | "The Star Chest awaits! Collect Stars to Open it and claim Rewards" |
| **Play Button** | Large yellow/gold rounded button with text "Play" |

### Coplay Setup Instructions

```
1. Create full-screen "StarChestOverlay" with semi-transparent black Image.

2. Add child Panel "StarChestPopup":
   - Size: ~600x1000px, centered
   - Image: white background with orange 9-slice border

3. Add "TitleBanner": gray rounded rectangle + Text "Star Chest"

4. Add Button "BtnCloseChest" at top-right (same X button style).

5. Add Panel "StarProgressBar":
   - HorizontalLayoutGroup
   - Image (gold star icon) on left
   - Slider "StarSlider": value = 223, maxValue = 500
     - Fill image: green
     - Add Text "223/500" overlaid on the slider (use Canvas overlay or child text)
   - Image (chest icon) on right end of slider

6. Add Panel "RewardPreview":
   - Image: tan/beige rounded border background
   - Child Text: "The Star Chest awaits!\nCollect Stars to\nOpen it and claim Rewards" (center aligned, bold)
   - Child Image "ChestIllustration": the treasure chest artwork
   - Floating item icons (arrows, magnet, star, snowflake) positioned around the chest using absolute anchored positions

7. Add Button "BtnPlay":
   - Image: large yellow/gold rounded rectangle
   - Text: "Play" (bold, white)
   - OnClick → close overlay and redirect to gameplay (Level select or current level)

8. BtnCloseChest onClick → deactivate StarChestOverlay.
9. Wire StarSlider value to a GameManager or PlayerData star count field at runtime.
```

---

## Shared UI Conventions

| Convention | Value |
|---|---|
| Canvas Scaler | Scale With Screen Size |
| Reference Resolution | 720 × 1560 |
| Popup overlay alpha | 0.6 (black) |
| Border style | 9-slice sprites with orange border |
| Title banner style | Gray rounded rectangle, bold dark text |
| Close button style | Brown square, "X" text or sprite |
| Primary button color | Green (play/buy) or Yellow/Gold (secondary actions) |
| Font style | Bold, rounded, cartoonish — e.g., Fredoka One or similar |
| Animation | DOTween scale punch on popup open (scale 0 → 1, ease OutBack) |
