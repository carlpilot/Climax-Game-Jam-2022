public struct SecretCode {

    static string[] privateCodes = { "tP95lQz0CkyNk7cR_YPPuAkN7wCkOxIkCu7WjI8E345g",
                                     "a3sgjx23xk-OKAFWvEhWVw9mdaNDxIy0yv4_gQqRyqzQ",
                                     "CYYeOGZQpUWa_BSwwLY61Q8YOkawTKake4BX50NW-0MA",
                                     "vzWB1d51ckKLDP5LREichQqwImfd6faUKKFap1svU24w",
                                     "S5MJs_iLdk6wgdtEScmdlQv6fv3Ukc5UeV5VYDfPLlEw",
                                     "znHFipmEb0qPGgqKBfBtLA-lV2DYvII0Wog6yGfNEOhw",
                                     "6rYrirqRcUaEUW7jCG-zDACFoPq__cqkOSbaCMUXYG8Q",
                                     "X0uifawHpkmBxqYT9KruWgiwScUD-XskeOzep7xP7AoA",
                                     "S_D_zjLOsEuHnAWn7cRbfwHJ6T2ZLZUkCdLgrFszeqgA"};
    static string[] publicCodes = { "622358b4778d3c8cfc1502d1",
                                    "622373fc8f40bb125853850d",
                                    "622395968f40bb125853c904",
                                    "622395ca8f40bb125853c969",
                                    "6223960f8f40bb125853c9fe",
                                    "6224eda78f40bb12585615cb",
                                    "6224edb88f40bb12585615eb",
                                    "6224edc58f40bb1258561603",
                                    "6224edd48f40bb1258561617"};

    public static string Public (int level) => publicCodes[level - 1];
    public static string Private (int level) => privateCodes[level - 1];
}
