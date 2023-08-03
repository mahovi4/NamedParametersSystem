using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamedParametersSystem;

public delegate void ParameterMessageHandler(string parameterName, string message);
public delegate void ParameterChangesHandler(string parameterName, object value);
