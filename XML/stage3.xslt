<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:f="http://example.com/functions">
  <xsl:output method="text"/>
  <xsl:template match="/root">
    <xsl:apply-templates select="stats"/>
    <xsl:apply-templates select="incidents"/>
  </xsl:template>
  <xsl:template match="incidents">
    <xsl:apply-templates select="incident"/>
  </xsl:template>
  <xsl:template match="stats">
                                                           
*******************************************************************************
*                               STATISTICS:                                   *
* Number of incidents: <xsl:value-of select="f:cut(numberOfIncidents, 55)" />*
* Most common category: <xsl:value-of select="f:cut(mostCommonCategory, 54)" />*
* Report generation date: <xsl:value-of select="f:cut(reportDate, 52)" />*
*******************************************************************************
  </xsl:template>
  <xsl:template match="incident">
+======================+===========================+==========================+
| ID: <xsl:value-of select="f:cut(incidentID, 17)" />| User: <xsl:value-of select="f:cut(endUser, 20)" />| Date: <xsl:value-of select="f:cut(dateOpened, 19)" />|
+======================+===========================+==========================+
| State: <xsl:value-of select="f:cut(state,14)" />| Category: <xsl:value-of select="f:cut(concat(category/mainCategory, '/', category/subcategory), 28+15)" />|
+======================+=========================+============================+
| Priority: <xsl:value-of select="f:cut(priority,10)" /> | Group: <xsl:value-of select="f:cut(assignmentGroup,17)" />| Phone: <xsl:value-of select="f:cut(concat(phoneNumber, '(PL)'),20)" />|
+======================+=========================+============================+
| Short: <xsl:value-of select="f:cut(shortDescription,69)" />|
+=============================================================================+
|                              Description:                                   |
| <xsl:value-of select="f:cut(description,76)" />|
+=============================================================================+
| Notes: <xsl:value-of select="f:cut(workNotes,69)" />|
+=============================================================================+
    
  </xsl:template>
</xsl:stylesheet>

