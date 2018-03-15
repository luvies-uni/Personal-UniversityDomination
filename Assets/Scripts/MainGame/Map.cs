﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Stores the data for the game map.
/// </summary>
public class Map : MonoBehaviour
{
    #region Unity Bindings

    [SerializeField] Sector[] m_sectors;

    #endregion

    #region Public Properties

    /// <summary>
    /// The sectors on the map.
    /// </summary>
    public Sector[] Sectors => m_sectors;

    /// <summary>
    /// The sectors that contain landmarks.
    /// </summary>
    public IEnumerable<Sector> LandmarkedSectors => Sectors.Where(s => s.Landmark != null);

    #endregion

    #region MonoBehaviour

    void Awake()
    {
        // init sectors here so they are ready for usage by Start methods
        for (int i = 0; i < m_sectors.Length; i++)
            m_sectors[i].Init(i);
    }

    #endregion

    #region Helper Methods

    /// <summary>
    /// Randomly allocates the PVC.
    /// </summary>
    public void AllocatePVC()
    {
        Sector lastPvcSector = Sectors.FirstOrDefault(s => s.HasPVC);
        Sector randomSector = Sectors.Random(s => s.AllowPVC && s != lastPvcSector);

        randomSector.HasPVC = true;
        Debug.Log("Allocated PVC at " + randomSector);
        if (lastPvcSector != null)
        {
            lastPvcSector.HasPVC = false;
            Debug.Log("Previous sector de-allocated");
        }
    }

    #endregion
}
