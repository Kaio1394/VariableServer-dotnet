using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using VariableServer.Models;

namespace VariableServer.Controllers
{
    [Route("[controller]/")]
    [ApiController]
    public class VariableController : ControllerBase
    {
        private static Dictionary<string, object> _dictVariables;

        public VariableController() 
        { 
            _dictVariables = Helper.Helper.LoadDictionaryFromFile(); 
        }

        #region GET
        [HttpGet("GetVariables")]
        public IActionResult GetVariables()
        {
            return Ok(_dictVariables);
        }
        [HttpGet("Get")]
        public IActionResult Get(string key)
        {
            if (_dictVariables.Keys.Contains(key))
            {
                var value = _dictVariables[key];
                var response = new
                {
                    key = key,
                    value = value
                };
                return Ok(response);
            }
            else
            {
                return NotFound("");
            }

        }
        #endregion

        #region POST
        [HttpPost("Set")]
        public IActionResult Set([FromBody] Variable variable)
        {
            if (string.IsNullOrEmpty(variable.Key)) BadRequest("The params 'key' can't empty");
            if (_dictVariables.Keys.Contains(variable.Key))
            {
                _dictVariables[variable.Key] = variable.Value;
                Helper.Helper.SaveDictionaryToFile(_dictVariables);
                return Ok("Update successful!");
            }
            else
            {
                _dictVariables.Add(variable.Key, variable.Value);
                Helper.Helper.SaveDictionaryToFile(_dictVariables);
                return Ok("Create successful!");
            }
        }
        #endregion

        #region DELETE
        [HttpDelete("Delete")]
        public IActionResult Delete(string key)
        {
            if (_dictVariables.Keys.Contains(key))
            {
                _dictVariables.Remove(key);
                Helper.Helper.SaveDictionaryToFile(_dictVariables);
                return Ok("Variable removed!");
            }
            else
            {
                return NotFound("Variable not found.");
            }
        }
        #endregion

    }
}
