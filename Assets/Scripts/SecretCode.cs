public struct SecretCode {

    static string[] privateCodes = { "tP95lQz0CkyNk7cR_YPPuAkN7wCkOxIkCu7WjI8E345g",
                                     "a3sgjx23xk-OKAFWvEhWVw9mdaNDxIy0yv4_gQqRyqzQ",
                                     "CYYeOGZQpUWa_BSwwLY61Q8YOkawTKake4BX50NW-0MA",
                                     "vzWB1d51ckKLDP5LREichQqwImfd6faUKKFap1svU24w",
                                     "S5MJs_iLdk6wgdtEScmdlQv6fv3Ukc5UeV5VYDfPLlEw"};
    static string[] publicCodes = { "622358b4778d3c8cfc1502d1",
                                    "622373fc8f40bb125853850d",
                                    "622395968f40bb125853c904",
                                    "622395ca8f40bb125853c969",
                                    "6223960f8f40bb125853c9fe"};

    public static string Public (int level) => publicCodes[level - 1];
    public static string Private (int level) => privateCodes[level - 1];
}
