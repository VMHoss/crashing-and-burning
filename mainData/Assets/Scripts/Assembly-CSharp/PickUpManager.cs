using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200010F RID: 271
public class PickUpManager
{
	// Token: 0x060007DE RID: 2014 RVA: 0x0003B6B8 File Offset: 0x000398B8
	public void Initialize(Transform aCameraTransform)
	{
		this.pFixRotation = Quaternion.Euler(-90f, 180f, 0f);
		this.pCameraTransform = aCameraTransform;
		this.UpdateUpgrade();
	}

	// Token: 0x060007DF RID: 2015 RVA: 0x0003B6E4 File Offset: 0x000398E4
	public void UpdateUpgrade()
	{
		this.pMagnetStrengthSQR = Data.Shared["Misc"].d["MagnetStrength"].f;
		if (Shop.GetUpgradeLevel("MagnetStrength") > 0)
		{
			this.pMagnetStrengthSQR += Data.Shared["Upgrades"].d["MagnetStrength"].d["Upgrade" + Shop.GetUpgradeLevel("MagnetStrength")].f;
		}
		this.pMagnetStrengthSQR *= this.pMagnetStrengthSQR;
	}

	// Token: 0x060007E0 RID: 2016 RVA: 0x0003B790 File Offset: 0x00039990
	public void UpdatePickUpsOnBlock(BlockData aBlockData)
	{
		Dictionary<GameObject, PickUpData> activePickUps = aBlockData.GetActivePickUps();
		if (activePickUps.Count == 0)
		{
			return;
		}
		Quaternion rotation = this.pCameraTransform.rotation * this.pFixRotation;
		Vector3 position = GameData.playerCarScript.transform.position;
		bool cashMagnet = GameData.playerCarScript.cashMagnet;
		foreach (KeyValuePair<GameObject, PickUpData> keyValuePair in activePickUps)
		{
			Transform transform = keyValuePair.Key.transform;
			if (!keyValuePair.Value.isPickedUp)
			{
				float num = (transform.position - position).sqrMagnitude;
				if (keyValuePair.Value.inMagnetField)
				{
					keyValuePair.Value.magnetProgression += Time.deltaTime;
					transform.position = Vector3.Lerp(transform.position, position, keyValuePair.Value.magnetProgression * keyValuePair.Value.magnetProgression);
					if ((double)keyValuePair.Value.magnetProgression >= 1.0)
					{
						num = 0f;
					}
				}
				if (num < 9f)
				{
					keyValuePair.Value.isPickedUp = true;
					if (keyValuePair.Value.mapIcon)
					{
						Scripts.interfaceScript.DestroyMapIcon(keyValuePair.Key);
					}
					Scripts.poolManager.ReturnToPool(keyValuePair.Key);
					string pickUpName = keyValuePair.Value.pickUpName;
					if (pickUpName == null)
					{
						goto IL_555;
					}
					if (PickUpManager.<>f__switch$map20 == null)
					{
						PickUpManager.<>f__switch$map20 = new Dictionary<string, int>(17)
						{
							{
								"Cash",
								0
							},
							{
								"CashBig",
								1
							},
							{
								"Nitro",
								2
							},
							{
								"Repair",
								3
							},
							{
								"FlameBurst",
								4
							},
							{
								"Detonator",
								5
							},
							{
								"NuclearDetonator",
								6
							},
							{
								"HiddenPackage",
								7
							},
							{
								"CashStash",
								8
							},
							{
								"Trophy",
								9
							},
							{
								"StyleQuadDamage",
								10
							},
							{
								"StyleStuntMan",
								11
							},
							{
								"StyleToxic",
								12
							},
							{
								"StyleDiablo",
								13
							},
							{
								"StyleDemon",
								13
							},
							{
								"StyleGold",
								14
							},
							{
								"StyleGolden",
								14
							}
						};
					}
					int num2;
					if (!PickUpManager.<>f__switch$map20.TryGetValue(pickUpName, out num2))
					{
						goto IL_555;
					}
					switch (num2)
					{
					case 0:
						Scripts.scoreManager.ObtainedCash();
						break;
					case 1:
						Scripts.scoreManager.ObtainedCashBig();
						break;
					case 2:
						GameData.playerCarScript.AddNitro();
						break;
					case 3:
						GameData.playerCarScript.RepairCar();
						break;
					case 4:
						ExplosionScript.AddExplosion("FlameBurst", transform.position);
						break;
					case 5:
						ExplosionScript.AddExplosion("Detonator", transform.position);
						break;
					case 6:
					{
						ExplosionScript.AddExplosion("NuclearDetonator", transform.position);
						GenericFunctionsScript.Fade("FromWhiteToZero");
						int item = int.Parse(keyValuePair.Value.name.Substring(24));
						if (!GameData.obtainedNuclearDetonators.Contains(item))
						{
							Scripts.medalsManager.UpdateMedal(7, 1);
							GameData.obtainedNuclearDetonators.Add(item);
						}
						break;
					}
					case 7:
					{
						int item2 = int.Parse(keyValuePair.Value.name.Substring(21));
						if (!GameData.obtainedHiddenPackages.Contains(item2))
						{
							Scripts.medalsManager.UpdateMedal(8, 1);
							GameData.obtainedHiddenPackages.Add(item2);
						}
						else
						{
							Debug.LogWarning("Uh oh, something went wrong, we already picked this HiddenPackage up! It should not have been spawned!");
						}
						break;
					}
					case 8:
					{
						Scripts.scoreManager.ObtainedCashStash();
						int item3 = int.Parse(keyValuePair.Value.name.Substring(17));
						if (!GameData.obtainedCashStashes.Contains(item3))
						{
							Scripts.medalsManager.UpdateMedal(9, 1);
							GameData.obtainedCashStashes.Add(item3);
						}
						else
						{
							Debug.LogWarning("Uh oh, something went wrong, we already picked this CashStash up! It should not have been spawned!");
						}
						break;
					}
					case 9:
					{
						int item4 = int.Parse(keyValuePair.Value.name.Substring(14));
						if (!GameData.obtainedSuperSpecialTrophies.Contains(item4))
						{
							Scripts.medalsManager.UpdateMedal(10, 1);
							GameData.obtainedSuperSpecialTrophies.Add(item4);
						}
						else
						{
							Debug.LogWarning("Uh oh, something went wrong, we already picked this super special Trophy up! It should not have been spawned!");
						}
						break;
					}
					case 10:
						GameData.playerCarScript.SetPickUpSuperPowerEffect("QuadDamage");
						break;
					case 11:
						GameData.playerCarScript.SetPickUpSuperPowerEffect("StuntMan");
						break;
					case 12:
						GameData.playerCarScript.SetPickUpSuperPowerEffect("Toxic");
						break;
					case 13:
						GameData.playerCarScript.SetPickUpSuperPowerEffect("Diablo");
						if (keyValuePair.Value.pickUpName == "StyleDemon")
						{
							Debug.LogWarning("JOEP: it's not Demon, but Diablo!");
						}
						break;
					case 14:
						GameData.playerCarScript.SetPickUpSuperPowerEffect("Golden");
						if (keyValuePair.Value.pickUpName == "StyleGold")
						{
							Debug.LogWarning("JOEP: it's not Gold, but Golden!");
						}
						break;
					default:
						goto IL_555;
					}
					IL_575:
					Scripts.trackScript.interfaceScript.interfacePanelScript.PickUp(keyValuePair.Value.pickUpName);
					continue;
					IL_555:
					Debug.LogWarning("Stijn will fix this, unhandled pickup: " + keyValuePair.Value.pickUpName);
					goto IL_575;
				}
				if (!keyValuePair.Value.inMagnetField && cashMagnet && num < this.pMagnetStrengthSQR && (keyValuePair.Value.pickUpName == "Cash" || keyValuePair.Value.pickUpName == "CashBig"))
				{
					keyValuePair.Value.inMagnetField = true;
				}
				transform.rotation = rotation;
			}
		}
	}

	// Token: 0x0400086D RID: 2157
	private Transform pCameraTransform;

	// Token: 0x0400086E RID: 2158
	private Quaternion pFixRotation;

	// Token: 0x0400086F RID: 2159
	private float pMagnetStrengthSQR = 20f;
}
