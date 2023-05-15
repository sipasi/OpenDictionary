using System.Globalization;

namespace OpenDictionary.ExternalAppTranslation;

public readonly record struct TranslationOptions(CultureInfo From, CultureInfo To);