﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ETL.SQLScripts {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ETL.SQLScripts.Resource", typeof(Resource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO TaxiTrips 
        ///                        (PickupDateTime, DropoffDateTime, PassengerCount, TripDistance, StoreAndFwdFlag, PULocationId, DOLocationId, FareAmount, TipAmount) 
        ///                        VALUES 
        ///                        (@PickupDateTime, @DropoffDateTime, @PassengerCount, @TripDistance, @StoreAndFwdFlag, @PULocationId, @DOLocationId, @FareAmount, @TipAmount).
        /// </summary>
        internal static string BulkInsertAsync {
            get {
                return ResourceManager.GetString("BulkInsertAsync", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE TABLE TaxiTrips (
        /// TaxiTripId INT PRIMARY KEY IDENTITY(1,1),
        ///    PickupDateTime DATETIME,
        ///    DropoffDateTime DATETIME,
        ///    PassengerCount INT,
        ///    TripDistance FLOAT,
        ///    StoreAndFwdFlag NVARCHAR(3),
        ///    PULocationId INT,
        ///    DOLocationId INT,
        ///    FareAmount DECIMAL(10, 2),
        ///    TipAmount DECIMAL(10, 2)
        ///);.
        /// </summary>
        internal static string CreateDatabase {
            get {
                return ResourceManager.GetString("CreateDatabase", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to CREATE INDEX IX_TaxiTrips_PULocationID ON TaxiTrips (PULocationID);
        ///CREATE INDEX IX_TaxiTrips_TripDistance ON TaxiTrips (TripDistance DESC);
        ///CREATE INDEX IX_TaxiTrips_DropoffPickupTime ON TaxiTrips (DropoffDateTime, PickupDateTime);
        ///CREATE INDEX IX_TaxiTrips_DOLocationID ON TaxiTrips (DOLocationID);
        ///CREATE INDEX IX_TaxiTrips_TipAmount ON TaxiTrips (TipAmount);.
        /// </summary>
        internal static string CreateIndexes {
            get {
                return ResourceManager.GetString("CreateIndexes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT TOP 1 *
        ///FROM TaxiTrips
        ///WHERE PickupDateTime = @PickupDateTime
        ///  AND DropoffDateTime = @DropoffDateTime
        ///  AND PassengerCount = @PassengerCount;.
        /// </summary>
        internal static string IsExist {
            get {
                return ResourceManager.GetString("IsExist", resourceCulture);
            }
        }
    }
}
